using UnityEngine;

namespace CodeBase.UI.Menu
{
    public class HeroesPreviewMainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _kingModel;
        [SerializeField] private GameObject _heroModel;
        [SerializeField] private GameObject _orbModel;
        [SerializeField] private GameObject _arrowTrapModel;
        [SerializeField] private GameObject _floorTrapModel;
     
        private GameObject _activeModel;

        public void ChangeModelToHero()
        {
            ChangeActiveModelTo(_heroModel);
        }

        public void ChangeModelToKing() =>
            ChangeActiveModelTo(_kingModel);    
        
        public void ChangeModelToOrb() =>
            ChangeActiveModelTo(_orbModel);      
        
        public void ChangeModelToArrowTrap() =>
            ChangeActiveModelTo(_arrowTrapModel);

        private void ChangeActiveModelTo(GameObject model)
        {
            if (_activeModel == model)
                return;

            if (_activeModel)
                _activeModel.SetActive(false);

            _activeModel = model;
            _activeModel.SetActive(true);
        }
    }
}
