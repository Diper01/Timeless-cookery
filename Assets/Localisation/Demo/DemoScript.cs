using UnityEngine;
using UnityEngine.UI;

public class DemoScript : MonoBehaviour
{
    [SerializeField] private Text sampleTxt;
    [SerializeField] private Text hallowWorldTxt;
	[SerializeField] private Text numberTxt;

	private readonly string _HelloWorld = "HelloWorld";

    private void Start()
    {
        sampleTxt.text = Localisation.GetString("samp", this);
        hallowWorldTxt.text = Localisation.GetString(_HelloWorld);
		numberTxt.text = Localisation.GetStringWithValues("nums_example", (Random.Range(1, 40)).ToString());
    }
}
