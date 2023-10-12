using System.Collections;
using System.Collections.Generic;
using UnityEngine;

        public class TestAudio : MonoBehaviour
        {
            public AudioController audioController;
            
            #region Unity Functions
#if UNITY_EDITOR
            private void Update()
            {
                if (audioController.debug)
                {
                    if (Input.GetKeyUp(KeyCode.T))
                    {
                        audioController.PlayAudio(AudioType.ST_06);
                    }
                    if (Input.GetKeyUp(KeyCode.Y))
                    {
                        audioController.StopAudio(AudioType.ST_06);
                    }
                    if (Input.GetKeyUp(KeyCode.U))
                    {
                        audioController.RestartAudio(AudioType.ST_06);
                    }
                    if (Input.GetKeyUp(KeyCode.I))
                    {
                        audioController.PlayAudio(AudioType.SFX_LASER);
                    }
                    if (Input.GetKeyUp(KeyCode.O))
                    {
                        audioController.StopAudio(AudioType.SFX_LASER);
                    }
                    if (Input.GetKeyUp(KeyCode.P))
                    {
                        audioController.RestartAudio(AudioType.SFX_LASER);
                    }
                }               
            }
#endif
            #endregion
        }
