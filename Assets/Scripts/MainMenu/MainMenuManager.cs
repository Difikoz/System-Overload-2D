using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class MainMenuManager : MonoBehaviour
    {
        [Header("Title Screen")]
        [SerializeField] private GameObject _titleScreenWindow;
        [SerializeField] private Button _titleScreenButtonStart;
        [Header("Main Menu")]
        [SerializeField] private GameObject _mainMenuWindow;
        [SerializeField] private Button _mainMenuButtonNewGame;
        [SerializeField] private Button _mainMenuButtonLoadGame;
        [SerializeField] private Button _mainMenuButtonQuitGame;

        private void Awake()
        {
            _titleScreenButtonStart.onClick.AddListener(OnTitleScreenButtonStartPressed);
            _mainMenuButtonNewGame.onClick.AddListener(OnMainMenuButtonNewGamePressed);
            _mainMenuButtonLoadGame.onClick.AddListener(OnMainMenuButtonLoadGamePressed);
            _mainMenuButtonQuitGame.onClick.AddListener(OnMainMenuButtonQuitGamePressed);
            _titleScreenWindow.SetActive(true);
            _titleScreenButtonStart.Select();
        }

        private void OnDestroy()
        {
            _titleScreenButtonStart.onClick.RemoveListener(OnTitleScreenButtonStartPressed);
            _mainMenuButtonNewGame.onClick.RemoveListener(OnMainMenuButtonNewGamePressed);
            _mainMenuButtonLoadGame.onClick.RemoveListener(OnMainMenuButtonLoadGamePressed);
            _mainMenuButtonQuitGame.onClick.RemoveListener(OnMainMenuButtonQuitGamePressed);
        }

        private void OnTitleScreenButtonStartPressed()
        {
            _titleScreenWindow.SetActive(false);
            _mainMenuWindow.SetActive(true);
            _mainMenuButtonNewGame.Select();
        }

        private void OnMainMenuButtonNewGamePressed()
        {
            SceneManager.LoadScene(1);
        }

        private void OnMainMenuButtonLoadGamePressed()
        {
            SceneManager.LoadScene(1);
        }

        private void OnMainMenuButtonQuitGamePressed()
        {
            Application.Quit();
        }
    }
}