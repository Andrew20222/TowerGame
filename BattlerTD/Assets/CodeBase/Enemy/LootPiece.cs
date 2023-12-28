using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using TMPro;
using UnityEngine;

namespace CodeBase.Enemy
{
  public class LootPiece : MonoBehaviour, ISavedProgress
  {
    [SerializeField] private float _speedRotation;
    public GameObject Skull;
    public GameObject PickupFxPrefab;
    public GameObject PickupPopup;
    public TextMeshPro LootText;

    private WorldData _worldData;
    private Loot _loot;

    private const float DelayBeforeDestroying = 1.5f;

    private string _id;

    private bool _pickedUp;

    public void Construct(WorldData worldData) => 
      _worldData = worldData;

    public void Initialize(Loot loot) => 
      _loot = loot;

    private void Start() => 
      _id = GetComponent<UniqueId>().Id;

    private void Update() =>
      Skull.transform.Rotate(_speedRotation * Time.deltaTime,0,0);

    private void OnTriggerEnter(Collider other)
    {
      if (!_pickedUp)
      {
        _pickedUp = true;
        Pickup();
      }
    }

    public void UpdateProgress(PlayerProgress progress)
    {
      if (_pickedUp)
        return;

      LootPieceDataDictionary lootPiecesOnScene = progress.WorldData.LootData.LootPiecesOnScene;

      if (!lootPiecesOnScene.Dictionary.ContainsKey(_id))
        lootPiecesOnScene.Dictionary
          .Add(_id, new LootPieceData(transform.position.AsVectorData(), _loot));
      Destroy(gameObject);
    }

    public void LoadProgress(PlayerProgress progress)
    {
    }

    private void Pickup()
    {
      UpdateWorldData();
      HideSkull();
      PlayPickupFx();
     // ShowText();

      Destroy(gameObject, DelayBeforeDestroying);
    }

    private void UpdateWorldData()
    {
      UpdateCollectedLootAmount();
      RemoveLootPieceFromSavedPieces();
    }

    private void UpdateCollectedLootAmount() =>
      _worldData.LootData.AddSilver(_loot.Value);

    private void RemoveLootPieceFromSavedPieces()
    {
      LootPieceDataDictionary savedLootPieces = _worldData.LootData.LootPiecesOnScene;

      if (savedLootPieces.Dictionary.ContainsKey(_id)) 
        savedLootPieces.Dictionary.Remove(_id);
    }

    private void HideSkull() =>
      Skull.SetActive(false);

    private void PlayPickupFx() =>
      Instantiate(PickupFxPrefab, transform.position, Quaternion.identity);

    private void ShowText()
    {
      LootText.text = $"{_loot.Value}";
      PickupPopup.SetActive(true);
    }
  }
}