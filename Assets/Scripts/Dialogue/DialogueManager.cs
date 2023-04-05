using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    bool dialoguing = false;
    bool done = true;
    bool faster = false;

    [Header("Settings")]

    public bool dontDestroyOnLoad;

    [Header("Containers")]

    public TMP_Text nameText;
    public TMP_Text dialogueText;
    [HideInInspector] public Queue<Dialogue.Sentence> sentences = new Queue<Dialogue.Sentence>();
    public GameObject charPrefab;
    public GameObject charParent;
    List<GameObject> instantiatedCharacters = new List<GameObject> { };
    public AudioSource audioSource;
    public AudioSource letterAudio;


    #region Events

    public static event Action OnStartDialogue;
    public static event Action OnStartSentence;

    public static event Action OnFinishSentence;
    public static event Action OnFinishDialogue;

    public static event Action OnSpecialSentenceStart;
    public static event Action OnSpecialSentenceEnd;

    #endregion


    #region Singleton
    [HideInInspector] public static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        if (dontDestroyOnLoad) DontDestroyOnLoad(instance);
    }
    #endregion
    void Update()
    {

        nameText.gameObject.SetActive(dialoguing);
        dialogueText.gameObject.SetActive(dialoguing);
        charParent.gameObject.SetActive(dialoguing);
        audioSource.gameObject.SetActive(dialoguing);
        letterAudio.gameObject.SetActive(dialoguing);

        if (!done && Input.anyKeyDown) faster = true;
        if (Input.anyKeyDown && dialoguing && done) DisplayNextSentence();
        
    }
    public void StartDialogue(Dialogue dialogue)
    {
        faster = false;
        dialoguing = true;
        sentences.Clear();
        OnStartDialogue?.Invoke();
        foreach (Dialogue.Sentence line in dialogue.lines) sentences.Enqueue(line);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {

        if (!done) return;
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        var sentence = sentences.Dequeue();
        nameText.text = sentence.name;
        nameText.transform.parent.localPosition = new Vector2(sentence.namePos, nameText.transform.parent.localPosition.y);
        StopAllCoroutines(); 
        audioSource.Stop();
        SetCharacters(sentence);
        
        audioSource.clip = sentence.sound;
        audioSource.Play();
        OnStartSentence?.Invoke();
        if(sentence.specialSentence) OnSpecialSentenceStart?.Invoke();
        StartCoroutine(TypeSentence(sentence.sentence, sentence.timeBetweenLetters, sentence.specialSentence));
    }
    void SetCharacters(Dialogue.Sentence sentence)
    {
        instantiatedCharacters.RemoveAll(item => item == null);
        foreach (var character in sentence.characters)
        {
            if (instantiatedCharacters.Count == 0)
            {
                GameObject prefab = Instantiate(charPrefab, charParent.transform);
                prefab.transform.localPosition = character.position;
                prefab.transform.localEulerAngles = character.rotation;
                prefab.transform.localScale = character.scale;
                prefab.GetComponentInChildren<Image>().sprite = character.sprite;
                prefab.name = character.name;
                prefab.GetComponentInChildren<Animator>().SetInteger("Animation", (int)character.animation);
                prefab.transform.GetChild(0).GetChild(0).transform.localPosition = character.emotionPosition;
                prefab.transform.GetChild(0).GetChild(1).transform.localPosition = character.emotionPosition;
                instantiatedCharacters.Add(prefab);
            }
            else
            {
                bool alreadyExists = false;
                foreach (GameObject currentChar in instantiatedCharacters)
                {
                    if (character.name == currentChar.name) 
                    {
                        currentChar.transform.localPosition = character.position;
                        currentChar.transform.localScale = character.scale;
                        currentChar.transform.localEulerAngles = character.rotation;
                        currentChar.GetComponentInChildren<Image>().sprite = character.sprite;
                        currentChar.transform.GetChild(0).GetChild(0).transform.localPosition = character.emotionPosition;
                        currentChar.transform.GetChild(0).GetChild(1).transform.localPosition = character.emotionPosition;
                        currentChar.GetComponentInChildren<Animator>().SetInteger("Animation", (int)character.animation);
                        alreadyExists = true; 
                    }
                    if (alreadyExists) break;
                }
                if (!alreadyExists)
                {
                    GameObject prefab = Instantiate(charPrefab, charParent.transform);
                    prefab.transform.localPosition = character.position;
                    prefab.transform.localScale = character.scale;
                    prefab.transform.localEulerAngles = character.rotation;
                    prefab.GetComponentInChildren<Image>().sprite = character.sprite;
                    prefab.name = character.name;
                    prefab.GetComponentInChildren<Animator>().SetInteger("Animation", (int)character.animation);
                    prefab.transform.GetChild(0).GetChild(0).transform.localPosition = character.emotionPosition;
                    prefab.transform.GetChild(0).GetChild(1).transform.localPosition = character.emotionPosition;
                    instantiatedCharacters.Add(prefab);
                }
            }
        }
        foreach(var instance in instantiatedCharacters)
        {
            bool exists = false;
            foreach(var character in sentence.characters)
            {
                if (character.sprite == instance.GetComponentInChildren<Image>().sprite)
                {
                    exists = true;
                }
                if(exists) break;
            }
            if(!exists) instance.GetComponentInChildren<Animator>().SetInteger("Animation", -1);
        }
    }

    void EndDialogue()
    {
        OnFinishDialogue?.Invoke();
        foreach (GameObject character in instantiatedCharacters)
        {
            if(character) character.GetComponentInChildren<Animator>().SetInteger("Animation", -1);
        }
        audioSource.Stop();
        instantiatedCharacters.Clear();
        done = true;
        dialoguing = false;
    }
    IEnumerator TypeSentence(string sentence, float time, bool special)
    {
        done = false;
        faster = false;
        dialogueText.text = "";
        foreach (char letter in sentence)
        {
            dialogueText.text += letter;
            letterAudio.Play();
            if (!faster) yield return new WaitForSeconds(time/1000);
            else yield return new WaitForSeconds(time/3000);
        }
        OnFinishSentence?.Invoke();
        if (special) OnSpecialSentenceEnd?.Invoke();
        done = true;
    }
}
