using System;
using CodeBase.Logic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CodeBase.Tower
{
	public abstract class PlaceableObject : MonoBehaviour
	{
		[SerializeField] private GameObject _placeableVisual;
		[SerializeField] private GameObject _gameVisual;
		[SerializeField] private TileBase _tileBase;
		[SerializeField] private TileBase _tileBaseNotMoveZone;
		[SerializeField] private TileBase _tileBaseAfterPlaced;
		[SerializeField] protected BoxCollider _collider;
		[SerializeField] protected TriggerObserver _triggerObserver;
		[SerializeField] protected bool _inAnotherTower;

		public int TowerCost;
		protected bool Placed { get; private set; }
		public Vector3Int Size { get; private set; }
		private Vector3[] Vertices = new Vector3[4];
		protected IBuildingService _buildingService;
		public TileBase TileBase
		{
			get
			{
				return _tileBase;
			}
		}
		public TileBase TileBaseAfterPlaced
		{
			get
			{
				return _tileBaseAfterPlaced;
			}
		}
		public bool InAnotherTower
		{
			get
			{
				return _inAnotherTower;
			}
		}
		public TileBase TileBaseNotMoveZone
		{
			get
			{
				return _tileBaseNotMoveZone;
			}
		}

		private void Start()
		{
			_triggerObserver.TriggerEnter += TriggerEnter;
			_triggerObserver.TriggerStay += TriggerStay;
			_triggerObserver.TriggerExit += TriggerExit;
		}

		private void OnDestroy()
		{
			_triggerObserver.TriggerEnter -= TriggerEnter;
			_triggerObserver.TriggerStay -= TriggerStay;
			_triggerObserver.TriggerExit -= TriggerExit;
		}

		private void TriggerExit(Collider obj)
		{
			_inAnotherTower = false;
		}

		private void TriggerStay(Collider obj)
		{
			_inAnotherTower = true;
		}

		private void TriggerEnter(Collider obj)
		{
			Debug.Log(obj.name);
			_inAnotherTower = true;
		}

		public void Construct(IBuildingService buildingService)
		{
			_buildingService = buildingService;
			GetColliderVertexPositionsLocal();
			CalculateSizeInCells();
		}
		
		public Vector3 GetStartPosition() =>
			transform.TransformPoint(Vertices[0]);

		private void GetColliderVertexPositionsLocal()
		{
			Vertices[0] = _collider.center + new Vector3(_collider.size.x, -_collider.size.y, _collider.size.z) * 0.5f;
			Vertices[1] = _collider.center + new Vector3(-_collider.size.x, -_collider.size.y, _collider.size.z) * 0.5f;
			Vertices[2] = _collider.center + new Vector3(-_collider.size.x, -_collider.size.y, -_collider.size.z) * 0.5f;
			Vertices[3] = _collider.center + new Vector3(_collider.size.x, -_collider.size.y, -_collider.size.z) * 0.5f;
		}

		private void CalculateSizeInCells()
		{
			Vector3Int[] vertices = new Vector3Int[Vertices.Length];

			for (int i = 0; i < vertices.Length; i++)
			{
				Vector3 worldPosition = transform.TransformPoint(Vertices[i]);
				vertices[i] = _buildingService.GridLayout.WorldToCell(worldPosition);
			}

			Size = new Vector3Int(Mathf.Abs((vertices[0] - vertices[1]).x), Mathf.Abs((vertices[0] - vertices[3]).y), 1);
		}

		public virtual void Place(ObjectDrag objectDrag)
		{
			_placeableVisual.SetActive(false);
			_gameVisual.SetActive(true);
			objectDrag.enabled = false;
			_collider.enabled = true;
			Placed = true;
		}
	}

}