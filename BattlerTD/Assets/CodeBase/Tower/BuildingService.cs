using System;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Inputs;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace CodeBase.Tower
{
	public class BuildingService : IBuildingService
	{
		private GridLayout _gridLayout;
		private Grid _grid;

		private MeshRenderer[] _ableToPlaceVisuals;
		private Tilemap _tilemap;

		private TileBase _tileWalls;
		private TileBase _tileFloors;

		private GameObject _iceOrb;
		private GameObject _fireOrb;

		private PlaceableObject _objectToPlace;
		private readonly IInputService _input;
		private readonly IGameFactory _factory;
		private readonly IStaticDataService _staticData;
		private readonly IPersistentProgressService _progressService;
		private bool _ableToPlace;
		private bool _isActive;
		private bool _isDraggingObject;
		private ObjectDrag _drag;
		private int _layerMask;
		public bool IsActive
		{
			get
			{
				return _isActive;
			}
			set
			{
				_isActive = value;
			}
		}
		public GridLayout GridLayout
		{
			get
			{
				return _gridLayout;
			}
		}
		public bool IsDraggingObject
		{
			get
			{
				return _isDraggingObject;
			}
		}

		public BuildingService(IInputService input, IGameFactory factory, IStaticDataService staticData, IPersistentProgressService progressService)
		{
			
			_input = input;
			_factory = factory;
			_staticData = staticData;
			_progressService = progressService;

			_input.TowerButtonUnpressed += TowerButtonUnpressed;
		}

		public void Init(Grid grid)
		{
			_layerMask = 1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Wall");
			_grid = Object.Instantiate(grid);
			Debug.Log(grid.name);
			_gridLayout = _grid.GetComponent<GridLayout>();
			_tilemap = _grid.GetComponentInChildren<Tilemap>();
		}

		private void TowerButtonUnpressed()
		{
			if (!_isActive)
				return;

			if (!_drag || !_objectToPlace)
				return;

			_isDraggingObject = false;
			_drag.StopDraggingCoroutine();
			if (CanBePlaced(_objectToPlace, _objectToPlace.InAnotherTower))
			{
				_progressService.Progress.WorldData.LootData.AddSilver(-_objectToPlace.TowerCost);
				_objectToPlace.Place(_drag);
				Vector3Int start = GridLayout.WorldToCell(_objectToPlace.GetStartPosition());
				TakeArea(start, _objectToPlace.Size);
				_objectToPlace = null;
			}
			else
			{
				Object.Destroy(_objectToPlace.gameObject, 0.2f);
			}
		}

		public Vector3 GetMouseWorldPosition()
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out RaycastHit raycastHit, _layerMask))
			{
				return raycastHit.point;
			}
			else
			{
				return Vector3.zero;
			}
		}

		public Vector3 SnapCoordinateToGrid(Vector3 position)
		{
			Vector3Int cellPos = GridLayout.WorldToCell(position);
			position = _grid.GetCellCenterWorld(cellPos);
			return position;
		}

		public bool CanBePlaced(PlaceableObject placeableObject, bool inAnotherCollider)
		{
			if (inAnotherCollider)
			{
				_ableToPlace = false;
			}
			else
			{
				BoundsInt area = new BoundsInt {
					position = GridLayout.WorldToCell(_objectToPlace.GetStartPosition()),
					size = placeableObject.Size,
				};

				TileBase[] baseArray = GetTilesBlock(area, _tilemap);

				foreach (TileBase tile in baseArray)
				{
					Debug.Log(tile);
					
					if (tile == placeableObject.TileBaseAfterPlaced || tile == null || tile != placeableObject.TileBase)
					{
						_ableToPlace = false;
						break;
					}
					else
					{
						_ableToPlace = true;
					}
				}
			}

			return _ableToPlace;
		}

		public bool CanBeMoved(PlaceableObject placeableObject)
		{
			bool canMove = true;
			BoundsInt area = new BoundsInt();

			area.position = GridLayout.WorldToCell(_objectToPlace.GetStartPosition());
			area.size = placeableObject.Size * 2;

			TileBase[] baseArray = GetTilesBlock(area, _tilemap);
			
			foreach (TileBase tile in baseArray)
			{
				if (tile == placeableObject.TileBaseNotMoveZone)
				{
					canMove = false;
					return canMove;
				}
			}
			
			return canMove;
		}

		public void InitTowerType(TowerType towerType, int towerCost)
		{
			if (!_isActive)
				return;
			
			if (towerType != TowerType.None)
			{
				InitializeWithObject(towerType);
			}
		}

		private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
		{
			TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];

			int counter = 0;

			foreach (Vector3Int areaPosition in area.allPositionsWithin)
			{
				Vector3Int position = new Vector3Int(areaPosition.x, areaPosition.y, 0);
				array[counter] = tilemap.GetTile(position);

				counter++;
			}

			return array;
		}

		private GameObject InitTower(TowerType towerType, Vector3 position) =>
			_factory.CreateTower(towerType, position, Quaternion.identity);

		private void InitializeWithObject(TowerType towerType)
		{
			
			Vector3 position = SnapCoordinateToGrid(GetMouseWorldPosition());

			_objectToPlace = InitTower(towerType, position).GetComponent<PlaceableObject>();

			RegisterDrag();
		}

		private void RegisterDrag()
		{
			_objectToPlace.Construct(this);
			_isDraggingObject = true;
			_drag = _objectToPlace.GetComponent<ObjectDrag>();
			_drag.Construct(this, _input);
			_drag.OnObjectTapped();
		}

		private void TakeArea(Vector3Int start, Vector3Int size) =>
			_tilemap.BoxFill(start, _objectToPlace.TileBaseAfterPlaced, start.x, start.y, start.x + size.x, start.y + size.y);
	}

}