using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMenu : MonoBehaviour
{
    public AccountsManager accountsManager;
    public GetInputField getInputField;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(buffer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator buffer()
    {
        yield return new WaitForSeconds(1f);
        accountsManager.loadAccount();
        StartCoroutine(getInputField.displayWarning(true));
    }
}
