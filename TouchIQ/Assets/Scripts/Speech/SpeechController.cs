using FrostweepGames.SpeechRecognition.Google.Cloud;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechController : MonoBehaviour
{

    private static SpeechController speechController;

    public static SpeechController GetInstance()
    {
        if (null == speechController)
        {
            speechController = new GameObject("SpeechController").AddComponent<SpeechController>();
        }
        return speechController;
    }

    public delegate void ContextSpeechDetected(List<string> contextWords);
    public event ContextSpeechDetected OnContextSpeechDetected;

    private ILowLevelSpeechRecognition _speechRecognition;

    private string _speechRecognitionResult = "";
    private string _speechContextHits = "";

    private string[] ContextPhrases = new string[] { "baby", "birthday", "ballet", "camp", "dog", "grandpa", "grampa", "vacation" };

    public void Initialize()
    {
        _speechRecognition = SpeechRecognitionModule.Instance;
        _speechRecognition.SetSpeechContext(ContextPhrases);
        (_speechRecognition as SpeechRecognitionModule).isRuntimeDetection = true;
        _speechRecognition.SpeechRecognizedSuccessEvent += SpeechRecognizedSuccessEventHandler;
        _speechRecognition.SpeechRecognizedFailedEvent += SpeechRecognizedFailedEventHandler;
        //StartCoroutine(RecordLoop());
        _speechRecognition.StartRuntimeRecord();
    }

    private IEnumerator RecordLoop()
    {
        _speechRecognition.StartRecord();
        yield return new WaitForSeconds(2);
        _speechRecognition.StopRecord();
        yield return null;
        StartCoroutine(RecordLoop());
    }

    private void SpeechRecognizedFailedEventHandler(string obj)
    {
        //_speechRecognitionState.color = Color.green;
        _speechRecognitionResult = "Speech Recognition failed with error: " + obj;

        //_startRecordButton.interactable = true;
        //_stopRecordButton.interactable = false;
    }

    private void SpeechRecognizedSuccessEventHandler(RecognitionResponse obj)
    {
        //_startRecordButton.interactable = true;

        //_speechRecognitionState.color = Color.green;

        if (obj != null && obj.results.Length > 0)
        {
            _speechRecognitionResult = "Speech Recognition succeeded! Detected Most useful: " + obj.results[0].alternatives[0].transcript;

            string other = "\nDetected alternative: ";

            foreach (var result in obj.results)
            {
                foreach (var alternative in result.alternatives)
                {
                    if (obj.results[0].alternatives[0] != alternative)
                        other += alternative.transcript + ", ";
                }
            }

            _speechRecognitionResult += other;
            List<string> contextWordsDetected = new List<string>();
            foreach(var contextWord in ContextPhrases)
            {
                if (obj.results[0].alternatives[0].transcript.Contains(contextWord))
                {
                    _speechContextHits += contextWord + ", ";
                    contextWordsDetected.Add(contextWord);
                }
            }
            if(contextWordsDetected.Count < 0 && null != OnContextSpeechDetected)
            {
                OnContextSpeechDetected(contextWordsDetected);
            }
        }
        else
        {
            _speechRecognitionResult = "Speech Recognition succeeded! Words are now detected.";

        }
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 100, 200, 200));
        GUILayout.Label(_speechRecognitionResult);
        GUILayout.Label(_speechContextHits);
        GUILayout.EndArea();
    }
}
