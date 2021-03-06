﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Methodyca.Core
{
    // This script creates a fade in/out visual through code. Methods here need to be called as coroutines
    public static class Fader
    {
        public static IEnumerator FadeIn(TMP_Text text, float fadeStart = 0f, float fadeEnd = 1f)
        {
            if (fadeStart > fadeEnd)
            {
                Debug.Log("Fade In didn't work. Make sure fadeStart is less than fadeEnd");
                yield break;
            }

            text.alpha = fadeStart;

            while (text.alpha < fadeEnd)
            {
                text.alpha += 0.05f;
                yield return new WaitForSeconds(0.05f);
            }
        }

        public static IEnumerator FadeIn(SpriteRenderer sprite, float fadeStart = 0f, float fadeEnd = 1f)
        {
            if (fadeStart > fadeEnd)
            {
                Debug.Log("Fade In didn't work. Make sure fadeStart is less than fadeEnd");
                yield break;
            }

            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, fadeStart);
            float alpha = fadeStart;

            while (alpha < fadeEnd)
            {
                alpha += 0.05f;
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
                yield return new WaitForSeconds(0.05f);
            }
        }

        public static IEnumerator FadeIn(Image image, float fadeStart = 0f, float fadeEnd = 1f)
        {
            if (fadeStart > fadeEnd)
            {
                Debug.Log("Fade In didn't work. Make sure fadeStart is less than fadeEnd");
                yield break;
            }

            image.color = new Color(image.color.r, image.color.g, image.color.b, fadeStart);
            float alpha = fadeStart;

            while (alpha < fadeEnd)
            {
                alpha += 0.05f;
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
                yield return new WaitForSeconds(0.05f);
            }
        }

        public static IEnumerator FadeOut(TMP_Text text, float fadeStart = 1f, float fadeEnd = 0f)
        {
            if (fadeStart < fadeEnd)
            {
                Debug.Log("Fade Out didn't work. Make sure fadeStart is more than fadeEnd");
                yield break;
            }

            text.alpha = fadeStart;

            while (text.alpha > fadeEnd)
            {
                text.alpha -= 0.05f;
                yield return new WaitForSeconds(0.05f);
            }
        }

        public static IEnumerator FadeOut(SpriteRenderer sprite, float fadeStart = 1f, float fadeEnd = 0f)
        {
            if (fadeStart < fadeEnd)
            {
                Debug.Log("Fade Out didn't work. Make sure fadeStart is more than fadeEnd");
                yield break;
            }

            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, fadeStart);
            float alpha = fadeStart;

            while (alpha > fadeEnd)
            {
                alpha -= 0.05f;
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
                yield return new WaitForSeconds(0.05f);
            }
        }

        public static IEnumerator FadeOut(Image image, float fadeStart = 1f, float fadeEnd = 0f)
        {
            if (fadeStart < fadeEnd)
            {
                Debug.Log("Fade Out didn't work. Make sure fadeStart is more than fadeEnd");
                yield break;
            }

            image.color = new Color(image.color.r, image.color.g, image.color.b, fadeStart);
            float alpha = fadeStart;

            while (alpha > fadeEnd)
            {
                alpha -= 0.05f;
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
                yield return new WaitForSeconds(0.05f);
            }

            yield return true;
        }
    }
}