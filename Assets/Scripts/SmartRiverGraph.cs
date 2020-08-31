using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq.Expressions;
using BarGraph.VittorCloud;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Assertions.Must;

public class SmartRiverGraph : MonoBehaviour
{
    #region publicVariables
    public List<BarGraphDataSet> sensorDataSet; // public data set for inserting data into the bar graph
    public Button btnHumidade, btnTempAmbiente, btnTempAgua, btnCondutividade, btnPh;
    public Button btnAnimated;
    public TextMeshProUGUI debugText;
    public Toggle isSemanal, isMensal;
    public GameObject ByMonth, ByYear;
    #endregion

    #region privateVariables
    private BarGraphGenerator barGraphGenerator;
    private Button btnNextM, btnBackM, btnNextY, btnBackY;
    private RectTransform transformButton;
    private TextMeshProUGUI txtMonth, txtYear;
    private int indexMonth;
    private int indexAtributo;
    private string year;
    private bool check = true;
    private bool[] isBarActive = new bool[5];
    private bool waitAnim = true;
    private List<string> mesesNome = new List<string>() { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };
    private List<string> semanaNome = new List<string>() { "1ª Semana", "2ª Semana", "3ª Semana", "4ª Semana" };
    #endregion

    void Awake()
    {        
        btnHumidade.onClick.AddListener(OnButtonHumidadeClick);
        btnTempAmbiente.onClick.AddListener(OnButtonTempAmbienteClick);
        btnTempAgua.onClick.AddListener(OnButtonTempAguaClick);
        btnCondutividade.onClick.AddListener(OnButtonCondutividadeClick);
        btnPh.onClick.AddListener(OnButtonPhClick);
        btnAnimated.onClick.AddListener(AnimateButton);
        transformButton = btnAnimated.GetComponent<RectTransform>();

        btnNextM = GameObject.Find("Canvas/PanelBottom/ByMonth/ButtonNextMonth").GetComponent<Button>();
        btnBackM = GameObject.Find("Canvas/PanelBottom/ByMonth/ButtonBackMonth").GetComponent<Button>();
        txtMonth = GameObject.Find("Canvas/PanelBottom/ByMonth/TextMonth").GetComponent<TextMeshProUGUI>();

        btnNextY = GameObject.Find("Canvas/PanelBottom/ByYear/ButtonNextYear").GetComponent<Button>();
        btnBackY = GameObject.Find("Canvas/PanelBottom/ByYear/ButtonBackYear").GetComponent<Button>();
        txtYear = GameObject.Find("Canvas/PanelBottom/ByYear/TextYear").GetComponent<TextMeshProUGUI>();

        btnNextM.onClick.AddListener(NextMonth);
        btnBackM.onClick.AddListener(BackMonth);

        btnNextY.onClick.AddListener(NextYear);
        btnBackY.onClick.AddListener(BackYear);

        txtMonth.text = "Janeiro";
        txtYear.text = "2020";
        indexMonth = 0;
        ByMonth.SetActive(true);
        ByYear.SetActive(true);

    }

    private void AnimateButton()
    {
        //        LeanTween.rotate(transformButton, -360.0f, 2f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.rotate(transformButton, -180.0f, 1f);
        //LeanTween.moveX(transformButton, 10f, 3f);
    }

    private void AnimateForNoData()
    {
        LeanTween.rotate(transformButton, 0.0f, 2f).setEase(LeanTweenType.easeOutElastic);

    }

    private void NextMonth()
    {

        if (isSemanal.isOn)
        {

            if (txtMonth.text == "Janeiro")
            {
                txtMonth.text = "Fevereiro";
                indexMonth = 1;
            }
            else if (txtMonth.text == "Fevereiro")
            {
                txtMonth.text = "Março";
                indexMonth = 2;
            }
            else if (txtMonth.text == "Março")
            {
                txtMonth.text = "Abril";
                indexMonth = 3;
            }
            else if (txtMonth.text == "Abril")
            {
                txtMonth.text = "Maio";
                indexMonth = 4;
            }
            else if (txtMonth.text == "Maio")
            {
                txtMonth.text = "Junho";
                indexMonth = 5;
            }
            else if (txtMonth.text == "Junho")
            {
                txtMonth.text = "Julho";
                indexMonth = 6;
            }
            else if (txtMonth.text == "Julho")
            {
                txtMonth.text = "Agosto";
                indexMonth = 7;
            }
            else if (txtMonth.text == "Agosto")
            {
                txtMonth.text = "Setembro";
                indexMonth = 8;
            }
            else if (txtMonth.text == "Setembro")
            {
                txtMonth.text = "Outubro";
                indexMonth = 9;
            }
            else if (txtMonth.text == "Outubro")
            {
                txtMonth.text = "Novembro";
                indexMonth = 10;
            }
            else if (txtMonth.text == "Novembro")
            {
                txtMonth.text = "Dezembro";
                indexMonth = 11;
            }

            Debug.Log("INDEX atributo: " + indexAtributo + " month: " + indexMonth);
            //GraphSemanal("Humidade", indexAtributo, "#3AFBEB");
        }

    }

    private void DisableBar()
    {
        int i = 0;
        while (i < 5)
        {
            isBarActive[i] = false;
            i++;
        }
    }

    private void NextYear()
    {
        DisableBar();
        string year = GetYear();
        int ano = int.Parse(year) + 1;
        txtYear.text = ano.ToString();
        Debug.Log("Ano Incrementado: " + ano);
    }

    private void BackYear()
    {
        DisableBar();
        string year = GetYear();
        int ano = int.Parse(year) - 1;
        txtYear.text = ano.ToString();
        Debug.Log("Ano Incrementado: " + ano);
    }

    private void BackMonth()
    {
        DisableBar();
        if (isSemanal.isOn)
        {

            if (txtMonth.text == "Dezembro")
            {
                txtMonth.text = "Novembro";
                indexMonth = 10;
            }
            else if (txtMonth.text == "Novembro")
            {
                txtMonth.text = "Outubro";
                indexMonth = 9;
            }
            else if (txtMonth.text == "Outubro")
            {
                txtMonth.text = "Setembro";
                indexMonth = 8;
            }
            else if (txtMonth.text == "Setembro")
            {
                txtMonth.text = "Agosto";
                indexMonth = 7;
            }
            else if (txtMonth.text == "Agosto")
            {
                txtMonth.text = "Julho";
                indexMonth = 6;
            }
            else if (txtMonth.text == "Julho")
            {
                txtMonth.text = "Junho";
                indexMonth = 5;
            }
            else if (txtMonth.text == "Junho")
            {
                txtMonth.text = "Maio";
                indexMonth = 4;
            }
            else if (txtMonth.text == "Maio")
            {
                txtMonth.text = "Abril";
                indexMonth = 3;
            }
            else if (txtMonth.text == "Abril")
            {
                txtMonth.text = "Março";
                indexMonth = 2;
            }
            else if (txtMonth.text == "Março")
            {
                txtMonth.text = "Fevereiro";
                indexMonth = 1;
            }
            else if (txtMonth.text == "Fevereiro")
            {
                txtMonth.text = "Janeiro";
                indexMonth = 0;
            }

            //Debug.Log("INDEX atributo: " + indexAtributo + " month: " + indexMonth);
            //GraphSemanal("Humidade", indexAtributo, "#3AFBEB");
        }
        DisableBar();
    }

    public string GetYear()
    {
        year = txtYear.text.ToString();
        return year;
    }

    public void CheckToggle()
    {
        DisableBar();
        indexMonth = 0;
        txtMonth.text = "Janeiro";
        Debug.Log("bool " + check.ToString());

        if (isMensal.isOn)
        {
            ByMonth.SetActive(false);
        }
        else
        {
            ByMonth.SetActive(true);
        }
    }

    public void OnButtonHumidadeClick()
    {
        if(isBarActive[0] == false && waitAnim)
        {
            indexAtributo = 0;
            if (isSemanal.isOn)
            {
                GraphSemanal("Humidade \n Relativa do Ar", indexAtributo, "#3AFBEB");
            }
            else if (isMensal.isOn)
            {
                //GraphMensal("Humidade \n Relativa do Ar", indexAtributo, "#3AFBEB");

                //Testando Media Mensal Anual
                GraphMensalAnual("Humidade \n Relativa do Ar", indexAtributo, "#3AFBEB");
            }

            isBarActive[0] = true;
            waitAnim = false;
            StartCoroutine(WaitBarAnimation());
        }
    }

    public void OnButtonTempAmbienteClick()
    {
        if(isBarActive[1] == false && waitAnim)
        {
            indexAtributo = 1;
            if (isSemanal.isOn)
            {
                GraphSemanal("Temp. Ambiente", indexAtributo, "#FFBB3D");
            }
            else if (isMensal.isOn)
            {
                //GraphMensal("Temp. Ambiente", indexAtributo, "#FFBB3D");
                GraphMensalAnual("Temp. Ambiente", indexAtributo, "#FFBB3D");

            }

            isBarActive[1] = true;
            waitAnim = false;
            StartCoroutine(WaitBarAnimation());
        }
    }

    public void OnButtonTempAguaClick()
    {
        if(isBarActive[2] == false && waitAnim)
        {
            indexAtributo = 2;
            if (isSemanal.isOn)
            {
                GraphSemanal("Temp. Agua", indexAtributo, "#4C9BFD");
            }
            else if (isMensal.isOn)
            {
                //            GraphMensal("Temp. Agua", indexAtributo, "#4C9BFD");
                GraphMensalAnual("Temp. Água", indexAtributo, "#4C9BFD");

            }

            isBarActive[2] = true;
            waitAnim = false;
            StartCoroutine(WaitBarAnimation());
        }
    }

    public void OnButtonPhClick()
    {
        if(isBarActive[3] == false && waitAnim)
        {
            indexAtributo = 4;
            if (isSemanal.isOn)
            {
                GraphSemanal("pH", indexAtributo, "#CC59FD");
            }
            else if (isMensal.isOn)
            {
                //GraphMensal("pH", indexAtributo, "#CC59FD");
                GraphMensalAnual("pH", indexAtributo, "#CC59FD");

            }

            isBarActive[3] = true;
            waitAnim = false;
            StartCoroutine(WaitBarAnimation());
        }
    }

    public void OnButtonCondutividadeClick()
    {
        if(isBarActive[4] == false && waitAnim)
        {
            indexAtributo = 3;
            if (isSemanal.isOn)
            {
                GraphSemanal("Condutividade", indexAtributo, "#F5E51F");
            }
            else if (isMensal.isOn)
            {
                //GraphMensal("Condutividade", indexAtributo, "#F5E51F");
                GraphMensalAnual("Condutividade", indexAtributo, "#F5E51F");

            }

            isBarActive[4] = true;
            waitAnim = false;
            StartCoroutine(WaitBarAnimation());
        }
    }
    
    IEnumerator WaitBarAnimation()
    {
        AnimateButton();
        yield return new WaitForSecondsRealtime(.8f);
        waitAnim = true;
    }

    public void GraphMensalAnual(string parametro, int atributo, string corHex)
    {
        XYBarValues newXY = new XYBarValues();
        List<XYBarValues> listXY = new List<XYBarValues>();
        listXY.Clear();
        Color color = new Color();
        ColorUtility.TryParseHtmlString(corHex, out color);
        int countNan = 0;
        int Ano = int.Parse(GetYear()) - 2020;
        float[,,] mediaMensalAnual = Sensor.getInstance().GetMediaGeralMesAno();

        Debug.Log("Ano: " + Ano);
        for (int y = Ano; y <= Ano ; y++)
        {
            for (int m = 0; m<= 11; m++)
            {

                Debug.Log("MensalAnual: " + mediaMensalAnual[atributo, m, y].ToString());

                if (mediaMensalAnual[atributo, m, y] != 0 && !float.IsNaN(mediaMensalAnual[atributo,m, y]))
                {
                    float media = (float)Math.Round(mediaMensalAnual[atributo, m , y], 2);
                    newXY = new XYBarValues(mesesNome[m], media);
                    listXY.Add(newXY);
                }
                else if (float.IsNaN(mediaMensalAnual[atributo, m, y]) || mediaMensalAnual[atributo, m, y] == 0)
                {
                    countNan++;
                    Debug.Log("countNan: " + countNan);
                }
            }

           
        }

        if (countNan == 12)
        {
                AnimateForNoData();
                debugText.text = "Não contem dados deste ano";
        }
        else
        {
            CreateGraphFromData(listXY, parametro, color);
        }
    }

    public void GraphMensal(string parametro, int atributo, string corHex)
    { 

        XYBarValues newXY = new XYBarValues();
        List<XYBarValues> listXY = new List<XYBarValues>();
        listXY.Clear();
        Color color = new Color();
        ColorUtility.TryParseHtmlString(corHex, out color);
        int countNan = 0;

        for (int i = 0; i <= 11; i++)
        {
            //colocar pra cima o mediaGealmes
            float[,] mediaGeraMes = Sensor.getInstance().GetMediaGeralMes();
            if (mediaGeraMes[atributo, i] != 0 && !float.IsNaN(mediaGeraMes[atributo, i]))
            {
                float media = (float)Math.Round(mediaGeraMes[atributo, i], 2);

                newXY = new XYBarValues(mesesNome[i], media);
                listXY.Add(newXY);
            }
            else if (float.IsNaN(mediaGeraMes[atributo,i]) || mediaGeraMes[atributo, i] == 0)
            {
                countNan++;
                Debug.Log("countNan: " + countNan);
            }
        }

        if (countNan == 12)
        {
            debugText.text = "Não contem dados deste ano"; 
        }
        else
        {
            CreateGraphFromData(listXY, parametro, color);
        }

    }

    public void GraphSemanal(string parametro, int atributo, string corHex)
    {
        Debug.Log("Graph Semanal");
        bool flag = true;
        XYBarValues newXY = new XYBarValues();
        List<XYBarValues> listXY = new List<XYBarValues>();
        listXY.Clear();
        Color color = new Color();
        ColorUtility.TryParseHtmlString(corHex, out color);
        int countNan = 0;
        int Ano = int.Parse(GetYear()) - 2020;
        for (int i = 0; i < 4; i++)
        {
            float[,,,] mediaSemanalMensal = Sensor.getInstance().GetMediaSemanalMensal();
            if (mediaSemanalMensal[i, indexMonth, atributo, Ano] != 0  && !float.IsNaN(mediaSemanalMensal[i, indexMonth, atributo, Ano]))
            {
                Debug.Log("MediaSemanalMensal: " + mediaSemanalMensal[i, indexMonth, atributo, Ano].ToString());
                float media = (float)Math.Round(mediaSemanalMensal[i, indexMonth, atributo, Ano], 1);  // [i, MES, Atributo]

                newXY = new XYBarValues(semanaNome[i], media);
                listXY.Add(newXY);

                debugText.text = "";

            }
            else if (float.IsNaN(mediaSemanalMensal[i, indexMonth, atributo, Ano]) || mediaSemanalMensal[i, indexMonth, atributo, Ano] == 0)
            {
                countNan++;
                Debug.Log("countNan: " + countNan);
            }
        }

        if (countNan <= 3)
        {
            CreateGraphFromData(listXY, parametro, color);
        }   
        else
        {
            debugText.text = "Não contem dados deste mês";
        }

    }


    public void CreateGraphFromData(List<XYBarValues> listXY, string parametro, Color sColor)
    {
        barGraphGenerator = GetComponent<BarGraphGenerator>();
        DestroyBefore();
        //Color color = new Color(UnityEngine.Random.Range(0F, 1F), UnityEngine.Random.Range(0, 1F), UnityEngine.Random.Range(0, 1F));
        BarGraphDataSet barHum = new BarGraphDataSet(parametro, sColor, listXY);
        sensorDataSet.Add(barHum);
        barGraphGenerator.GeneratBarGraph(sensorDataSet);
    }

    //Aqui resolve o bug de criar periodo em cima de periodo
    public void DestroyBefore()
    {
        Destroy(GameObject.Find("GraphBox(Clone)"));

/*        
 *        foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject))) //search all object in scene
        {
            if(obj.name == "GraphBox(Clone)")
            {
                Destroy(GameObject.Find("GraphBox(Clone)"));
            }
        }*/
    }

    public void ClearDataset()
    {
        DisableBar();
        sensorDataSet.Clear();
        Sensor.getInstance().getArray().Clear();
        Sensor.getInstance().DeleteList();
    }

}



