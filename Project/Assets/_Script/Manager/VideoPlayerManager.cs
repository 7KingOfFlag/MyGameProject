using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

namespace OurGameName.Manager
{
    public class VideoPlayerManager : MonoBehaviour
    {
        
        public VideoPlayer videoPlayer;
        private float ShowSpeed = 0.8f;
        bool canShow;
        Image[] images;

        private void Awake()
        {
            images = GetComponentsInChildren<Image>();
            canShow = false;
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            if (!videoPlayer.isPlaying)
            {
                ShowText();
            }
            else
            {
                Hied();
            }
        }

        private void ShowText()
        {
            Image image = images[0];

            Color color = image.color;
            color.a = Mathf.Lerp(color.a, 1, ShowSpeed * Time.deltaTime);
            foreach (var item in images)
            {
                item.color = color;
            }
        }

        private void Hied()
        {
            foreach (var item in images)
            {
                Color color = item.color;
                color.a = 0;
                item.color = color;
            }
        }
    }

}
