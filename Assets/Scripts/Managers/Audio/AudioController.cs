using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioController : MonoBehaviour
        {
            public static AudioController instance;

            public bool debug;
            public AudioTrack[] tracks;

            private Hashtable m_AudioTable;
            private Hashtable m_JobTable;

            [System.Serializable]
            public class AudioObject
            {
                public AudioType type;
                public AudioClip clip;
            }

            [System.Serializable]
            public class AudioTrack
            {
                public AudioSource source;
                public AudioObject[] audio;
            }

            private class AudioJob
            {
                public AudioAction action;
                public AudioType type;

                public AudioJob(AudioAction _action,AudioType _type)
                {
                    action = _action;
                    type = _type;
                }
            }

    private enum AudioAction
            {
                START,
                STOP,
                RESTART,
            }
    #region Unity Functions
    private void Awake()
            {
                if (!instance)
                {
                    Configure();
                }
            }
    private void OnDisable()
            {
                Dispose();
            }
    private void Start()
    {
        string nombreEscena = SceneManager.GetActiveScene().name;
        if(nombreEscena.Equals("Menu principal"))
        {
            PlayAudio(AudioType.TITLE);
            return;
        }
        if(nombreEscena.Equals("CinematicaPrologo"))
        {
            PlayAudio(AudioType.PROLOGO);
            return;
        }
        if (nombreEscena.Equals("Tutorial"))
        {
            PlayAudio(AudioType.ST_00);
            return;
        }
        if (nombreEscena.Equals("Nivel 1"))
        {
            PlayAudio(AudioType.ST_01);
            return;
        }
        if (nombreEscena.Equals("Nivel 2"))
        {
            PlayAudio(AudioType.ST_02);
            return;
        }
        if (nombreEscena.Equals("Nivel 3"))
        {
            PlayAudio(AudioType.ST_03);
            return;
        }
        if (nombreEscena.Equals("Nivel 4"))
        {
            PlayAudio(AudioType.ST_04);
            return;
        }
        
        
    }
            #endregion

    #region Public Functions
    public void PlayAudio(AudioType _type)
            {
                AddJob(new AudioJob(AudioAction.START, _type));
            }
    public void StopAudio(AudioType _type)
            {
                AddJob(new AudioJob(AudioAction.STOP, _type));
            }
    public void RestartAudio(AudioType _type)
            {
                AddJob(new AudioJob(AudioAction.RESTART, _type));
            }
    public void StopAll()
    {
        foreach(AudioType _type in m_AudioTable)
        {
            StopAudio(_type);
        }
    }
    #endregion

            #region Private Functions
            private void Configure()
            {
                instance = this;
                m_AudioTable = new Hashtable();
                m_JobTable = new Hashtable();
                GenerateAudioTable();
            }
            private void Dispose()
            {
                foreach(DictionaryEntry _entry in m_JobTable)
                {
                    IEnumerator _job = (IEnumerator)_entry.Value;
                    StopCoroutine(_job);
                }
            }
            private void GenerateAudioTable()
            {
                foreach(AudioTrack _track in tracks)
                {
                    foreach(AudioObject _obj in _track.audio)
                    {
                        if (m_AudioTable.ContainsKey(_obj.type))
                        {
                            LogWarning("Estas intentando registrar audio [" + _obj.type + "] que ya ha sido registrado.");
                        }
                        else
                        {
                            m_AudioTable.Add(_obj.type, _track);
                            Log("Registrando audio [" + _obj.type + "].");
                        }
                    }
                }
            }
            private IEnumerator RunAudioJob(AudioJob _job)
            {
                AudioTrack _track = (AudioTrack)m_AudioTable[_job.type];
                _track.source.clip = GetAudioClipFromAudioTrack(_job.type, _track);
                switch (_job.action)
                {
                    case AudioAction.START:
                        _track.source.Play();
                        break;
                    case AudioAction.STOP:
                        _track.source.Stop();
                        break;
                    case AudioAction.RESTART:
                        _track.source.Stop();
                        _track.source.Play();
                        break;
                }
                m_JobTable.Remove(_job.type);
                Log("Conteo de trabajos: " + m_JobTable.Count);

                yield return null;
            }
            private void AddJob(AudioJob _job)
            {
                //remover conflictos de trabajos(jobs)
                RemoveConflictingJobs(_job.type);

                //comenzar el trabajo(job)
                IEnumerator _jobRunner = RunAudioJob(_job);
                m_JobTable.Add(_job.type, _jobRunner);
                StartCoroutine(_jobRunner);
                Log("Comenzando trabajo en [" + _job.type + "] con operación: " + _job.action);
            }
            private void RemoveJob(AudioType _type)
            {
                if (!m_JobTable.ContainsKey(_type))
                {
                    LogWarning("Estás intentando detener un trabajo [" + _type + "] que no esta corriendo.");
                    return;
                }
                IEnumerator _runningJob = (IEnumerator)m_JobTable[_type];
                StopCoroutine(_runningJob);
                m_JobTable.Remove(_type);
            }
            private void RemoveConflictingJobs(AudioType _type)
            {
                if (m_JobTable.ContainsKey(_type))
                {
                    RemoveJob(_type);
                }
                AudioType _conflictingAudio = AudioType.None;
                foreach(DictionaryEntry _entry in m_JobTable)
                {
                    AudioType _audioType = (AudioType)_entry.Key;
                    AudioTrack _audioTrackInUse = (AudioTrack)m_AudioTable[_audioType];
                    AudioTrack _audioTrackNeeded = (AudioTrack)m_AudioTable[_type];
                    if (_audioTrackNeeded.source == _audioTrackInUse.source)
                    {
                        _conflictingAudio = _audioType;
                    }

                }
                if (_conflictingAudio != AudioType.None)
                {
                    RemoveJob(_conflictingAudio);
                }
            }
            public AudioClip GetAudioClipFromAudioTrack(AudioType _type,AudioTrack _track)
            {
                foreach(AudioObject _obj in _track.audio)
                {
                    if (_obj.type == _type)
                    {
                        return _obj.clip;
                    }
                }
                return null;
            }
            private void Log(string _msg)
            {
                if (!debug) return;
                Debug.Log("[Audio Controller]: " + _msg);
            }
            private void LogWarning(string _msg)
            {
                if (!debug) return;
                Debug.LogWarning("[Audio Controller]: " + _msg);
            }
            #endregion
        }

