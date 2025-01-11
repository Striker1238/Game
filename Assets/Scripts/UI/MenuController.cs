using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace UI
{
    public class MenuController: MonoBehaviour
    {
        [Header("MenuUI")]
        public GameObject camera;
        public float speedCamera;
        public Vector2 maxPosition;

        public GameObject menuPage;
        public GameObject settingsPage;

        private Sequence settingsAnim;
        private Sequence menuAnim;

        private Sequence cameraSequence;
        private Coroutine cameraMovementCoroutine;
        private bool direction = false;

        
        public void Start()
        {
            cameraMovementCoroutine = StartCoroutine(CameraMovement());

            

            
            
        }
        public IEnumerator CameraMovement()
        {
            while (true)
            {
                float targetPositionX = direction
                    ? camera.transform.position.x + maxPosition.y
                    : camera.transform.position.x + maxPosition.x;

                cameraSequence = DOTween.Sequence();
                cameraSequence
                    .Append(camera.transform
                    .DOMoveX(targetPositionX, speedCamera)
                    .SetEase(Ease.InOutSine));

                yield return cameraSequence.WaitForCompletion();
                direction = !direction;
            }
        }


        public void OpenSettings()
        {

            settingsPage.SetActive(true);
            // Создаем анимацию для открытия настроек
            settingsAnim = DOTween.Sequence();
            settingsAnim.Append(menuPage.transform.DOLocalMoveX(2000, 1f).From(0).SetEase(Ease.InBack))
                .Join(menuPage.GetComponent<CanvasGroup>().DOFade(0, 1f).SetEase(Ease.InQuart)
                .OnComplete(() =>
                {
                    StopCoroutine(cameraMovementCoroutine);
                    cameraSequence?.Kill();
                }))
                .Append(camera.transform.DOMoveX(-30, speedCamera / 50).SetEase(Ease.InOutSine))
                .Join(settingsPage.GetComponent<CanvasGroup>().DOFade(1, 1f).SetEase(Ease.OutCubic))
                .Join(settingsPage.transform.DOLocalMoveX(0, 1f).From(-550).SetEase(Ease.OutBack))
                .OnComplete(() =>
                {
                    menuPage.SetActive(false);
                    cameraMovementCoroutine = StartCoroutine(CameraMovement());
                });
        }

        public void CloseSettings()
        {
            // Создаем анимацию для открытия основного меню
            menuAnim = DOTween.Sequence();
            menuPage.SetActive(true);
            menuAnim.Append(settingsPage.GetComponent<CanvasGroup>().DOFade(0, 1f))
                .Join(settingsPage.transform.DOLocalMoveX(-550, 1f).From(0).SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    StopCoroutine(cameraMovementCoroutine);
                    cameraSequence?.Kill();
                }))
                .Append(camera.transform.DOMoveX(30, speedCamera / 50).SetEase(Ease.InOutSine))
                .Join(menuPage.transform.DOLocalMoveX(0, 1f).From(2000).SetEase(Ease.OutBack))
                .Join(menuPage.GetComponent<CanvasGroup>().DOFade(1, 1f).SetEase(Ease.OutQuart))
                .OnComplete(() =>
                {
                    settingsPage.SetActive(false);
                    cameraMovementCoroutine = StartCoroutine(CameraMovement());
                });
        }

        public void Exit() => Application.Quit();

    }

}
