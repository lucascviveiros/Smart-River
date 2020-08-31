using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region publicVariables
    public GameObject panelCenter;
    public TextMeshProUGUI textInfoTitle, textInfoDesc;
    public TextMeshProUGUI textOtherTitle, textOtherDesc;
    public Button btnHideInfo, btnHideOther;
    #endregion

    #region privaVariables
    private Button btnHumidade, btnTemp, btnAgua, btnPh, btnCond;
    private Button btnO2, btnNitritos, btnCQO, btnFosfatos, btnCBO;
    private RectTransform leftCanvas, rightCanvas;
    public Canvas infoCanvas;
    private float xValue = 1333.0f;
    private bool isOpenL = false;
    private bool isOpenR = false;
    #endregion

    void Start()
    {     
        ///Left Canvas
        leftCanvas = GameObject.Find("Canvas/InfoLeftCanvas").GetComponent<Canvas>().GetComponent<RectTransform>();

        ///Left Buttons
        btnHumidade = GameObject.Find("Canvas/PanelLeft/ButtonHumidade").GetComponent<Button>();
        btnTemp = GameObject.Find("Canvas/PanelLeft/ButtonTempAmb").GetComponent<Button>();
        btnAgua = GameObject.Find("Canvas/PanelLeft/ButtonTempAgua").GetComponent<Button>();
        btnPh = GameObject.Find("Canvas/PanelLeft/ButtonPh").GetComponent<Button>();
        btnCond = GameObject.Find("Canvas/PanelLeft/ButtonCondutividade").GetComponent<Button>();

        ///Left Methods
        btnHumidade.onClick.AddListener(OnClickHumidade);
        btnTemp.onClick.AddListener(OnClickTempAmbiente);
        btnAgua.onClick.AddListener(OnClickTempAgua);
        btnPh.onClick.AddListener(OnClickPh);
        btnCond.onClick.AddListener(OnClickCondutividade);

        ///Hide Left
        btnHideInfo.onClick.AddListener(HideLeftInformation);

        ///Right Canvas
        rightCanvas = GameObject.Find("Canvas/InfoRightCanvas").GetComponent<Canvas>().GetComponent<RectTransform>();

        ///Right Buttons
        btnO2 = GameObject.Find("Canvas/PanelRight/ButtonO2").GetComponent<Button>();
        btnNitritos = GameObject.Find("Canvas/PanelRight/ButtonNitrito").GetComponent<Button>();
        btnCQO = GameObject.Find("Canvas/PanelRight/ButtonCQO").GetComponent<Button>();
        btnFosfatos = GameObject.Find("Canvas/PanelRight/ButtonFosfatos").GetComponent<Button>();
        btnCBO = GameObject.Find("Canvas/PanelRight/ButtonCBO5").GetComponent<Button>();

        ///Right Methods
        btnO2.onClick.AddListener(OnClickOxigen2);
        btnNitritos.onClick.AddListener(OnClickNitritos);
        btnCQO.onClick.AddListener(OnClickCQO);
        btnFosfatos.onClick.AddListener(OnClickFosfatos);
        btnCBO.onClick.AddListener(OnClickCBO);

        ///Hide Right
        btnHideOther.onClick.AddListener(HideRightOther);

        infoCanvas.enabled = true;

    }

    //Hide Methods
    public void HideLeftInformation()
    {
        if (isOpenL)
        {
            LeanTween.move(leftCanvas, new Vector3(-xValue, 0.0f, 0f), 1f).setCanvasMoveX().setDelay(0.1f);
            panelCenter.SetActive(true);
            isOpenL = false;
        }
    }
    public void HideRightOther()
    {
        if (isOpenR)
        {
            LeanTween.move(rightCanvas, new Vector3(xValue, 0.0f, 0f), 1f).setCanvasMoveX().setDelay(0.1f);
            panelCenter.SetActive(true);
            isOpenR = false;
        }
 
    }

    //Left Methods
    public void OnClickHumidade()
    {
        ChangeInfo(0);
    }

    public void OnClickTempAmbiente()
    {
        ChangeInfo(1);
    }

    public void OnClickTempAgua()
    {
        ChangeInfo(2);
    }

    public void OnClickPh()
    {
        ChangeInfo(3);
    }

    public void OnClickCondutividade()
    {
        ChangeInfo(4);
    }


    //Right Methods
    public void OnClickOxigen2()
    {
        ChangeOther(0);
    }

    public void OnClickNitritos()
    {
        ChangeOther(1);
    }

    public void OnClickCQO()
    {
        ChangeOther(2);
    }

    public void OnClickFosfatos()
    {
        ChangeOther(3);
    }

    public void OnClickCBO()
    {
        ChangeOther(4);
    }


    public void ChangeInfo(int indexInfo)
    {
        if(indexInfo == 0)
        {
            textInfoTitle.text = "Humidade Relativa do Ar";
            textInfoDesc.text = "O que é?\nÉ a quantidade de água na forma de vapor que existe na atmosfera no momento em relação ao total máximo que poderia existir, na temperatura observada. A Quantidade de vapor de água presente no ar, expressa em percentagem da máxima quantidade de vapor de água que esse ar poderia conter à mesma temperatura. É a relação em percentagem(%) entre a quantidade de vapor de água contida no ar(humidade absoluta) e a que existiria se à mesma temperatura o ar estivesse saturado. \n\nPorque é importante medir? \nA humidade relativa é um parâmetro importante para os estudos sobre o clima, as suas variações são responsáveis por inúmeras situações, mais ou menos negativas, que afetam a vida dos utilizadores. Logo torna-se essencial conhecer as formas de a poder controlar a fim de conseguir garantir a satisfação dos utilizadores em áreas como saúde, conforto, economia energética assim como evitar a degradação precoce dos materiais. \n\nQuais os valores de referência? \nIdeal - entre 40 % e 60 %";
        }
        else if(indexInfo == 1)
        {
            textInfoTitle.text = "Temperatura Ambiente";
            textInfoDesc.text = "O que é?\n\nA temperatura ambiente é o termo que se refere à temperatura do ar em um determinando ambiente, sem a influência da troca de calor com fontes calóricas.Também é considerada temperatura ambiente em termos laboratoriais a temperatura média de 22⁰C ou 23⁰C. \n\n Porque é importante medir? \n\n A medição da temperatura é de uma importância fundamental em diversos setores(alimentação, agricultura, medicamentos, laboratórios, ETC), garantindo eficácia e segurança nos processos realizados. \n\n Quais os valores de referência? \n\n VR: 25⁰C";
        }
        else if (indexInfo == 2)
        {
            textInfoTitle.text = "Temperatura da Água";
            textInfoDesc.text = "O que é?\n\nA temperatura é um parâmetro físico que permite medir as sensações de calor e frio. Do ponto de vista microscópico, a temperatura é considerada uma representação da energia cinética interna média das moléculas que compõem a água.Essa energia cinética se manifesta na forma de agitação térmica, que resulta da colisão entre as moléculas que compõem a água.\n\n Porque é importante medir?\n\n É importante conhecer a temperatura da água, pois pode ajudar a prever e confirmar outras condições da água(influencia diretamente outros fatores da qualidade da água como oxigénio dissolvido(OD), carência a bioquímica de oxigênio(CBO5) e sobrevivência de algumas espécies biológicas).Pode afetar as taxas de reprodução de algumas espécies aquáticas, e temperaturas mais altas podem aumentar a suscetibilidade de organismos aquáticos a doenças porque bactérias e outros organismos causadores de doenças crescem mais rapidamente nas águas quentes. \n\nQuais os valores de referência? \n\nVMA: 30⁰C - limites estes descritos no Decreto-lei 236 / 1998."; 
        }
        else if (indexInfo == 3)
        {
            textInfoTitle.text = "pH";
            textInfoDesc.text = "O que é?\n\nO pH é um parâmetro mensurável entre os valores de 0 e 14, desde que a concentração da solução não exceda 1M.As soluções com pH < 7 são ácidas, enquanto as com pH > 7 são alcalinas. Mede - se as mudanças na atividade dos iões hidrogénio na solução.Pode ser condicionante para a viabilidade dos seres vivos num determinado local.\n\nPorque é importante medir? \n\nA monitorização do pH é importante pois pode ser um indicador de contaminações pontuais de químicos ou matéria orgânica, sendo um condicionante para a abundância de comunidades biológicas ou microbiológicas. \n\nQuais os valores de referência? \n\nNo caso das águas superficiais, os limites estabelecidos para permitir a existência de fauna e flora num ecossistema aquático são: VMA - pH 5.0 e 9.0 - limites estes descritos no Decreto-lei 236 / 1998.";
        }
        else if (indexInfo == 4)
        {
            textInfoTitle.text = "Condutividade";
            textInfoDesc.text = "O que é?\n\nA condutividade elétrica da água representa a facilidade ou dificuldade de passagem da eletricidade na água.  Os compostos orgânicos e inorgânicos contribuem ou interferem na condutividade. É um indicativo da qualidade da água. \n\nPorque é importante medir? \n\nA condutividade é um parâmetro que está relacionado diretamente com a contaminação de um curso de água.Quanto maior a condutividade, maior o nível de contaminação. \n\nQuais os valores de referência? \n\nOs níveis de condutividade base / referência para sistemas pristinos não são todos iguais devido às diferentes características geológicas do sedimento originário do leito do rio e outros fatores que terão de ser tidos em conta(INAG, 2009) \n\nVMR: 1000 uS / cm a 20 ⁰C(Objetivos ambientais de qualidade mínima para águas para águas destinadas à produção de água para consumo).";
        }

        isOpenL = true;
        HideRightOther();
        panelCenter.SetActive(false);
        LeanTween.move(leftCanvas, new Vector3(xValue, 0.0f, 0f), 1f).setCanvasMoveX().setDelay(0.1f);
             
    }

    public void ChangeOther(int indexOther)
    {
        if(indexOther == 0)
        {
            textOtherTitle.text = "O2 Dissolvido";
            textOtherDesc.text = "O que é?\n\nO O2 dissolvido provem da dissolução do O2 atmosférico, naturalmente ou artificialmente e também da produção por alguns microrganismos vivos na água.\n\nPorque é importante medir?\n\nÉ um elemento limitante podendo condicionar o desenvolvimento ou mesmo sobrevivência de qualquer elemento na fauna e flora de um local, logo é de extrema importância para comunidades ecossistema aquático, daí que a monitorização das taxas de saturação de O2 e da concentração de O2 dissolvido sejam tão importantes.\n\nQuais os valores de referência?\n\nNo caso das águas superficiais, os limites estabelecidos para permitir a existência de fauna e flora num ecossistema aquático é: VMA - 50 % da saturação de O2 ou 5 mg O2/ L - limites estes descritos no Decreto-lei 236 / 1998.";
        }
        else if (indexOther == 1)
        {
            textOtherTitle.text = "Nitritos e Nitratos";
            textOtherDesc.text = "O que é?\n\nO azoto é um dos elementos necessários à vida e essenciais ao desenvolvimento das plantas e dos animais, por fazer parte da constituição de diversas moléculas.É um elemento não metálico, presente na natureza sob forma gasosa(constituindo 80 % da atmosfera), sob a forma de compostos orgânicos e sob a forma mineral(nitratos, entre outros).As diferentes formas são constituintes azotados cuja a presença é natural no meio ambiente em consequência do ciclo de azoto. Os nitratos(NO3-) são um dos compostos azotados de maior importância, na medida em que são um componente essencial à formação da biomassa das plantas e dos animais, mas por outro lado, são também um contaminante relevante nas águas superficiais e subterrâneas(ex.: abundância nos fertilizantes aplicados na agricultura, resíduos industriais e domésticos).Os nitritos(NO2-) são produto da oxidação do amónio ou da redução dos nitratos.Na água, em condições de oxidação normais, a conversão dos nitritos em nitratos é quase imediata. A sua presença na água deverá, por isso ser pontual e temporária.\n\nPorque é importante medir?\n\nA elevada concentração de nitratos pode ser responsável pelo desenvolvimento excessivo de vegetação aquática, eventuais maus odores.\n\nQuais os valores de referência?\n\nVMA:25 mg NO3/ L";
        }
        else if (indexOther == 2)
        {
            textOtherTitle.text = "CQO (Carência Química de Oxigénio)";
            textOtherDesc.text = "O que é?\n\nO CQO é um parâmetro usado para medir a matéria orgânica de uma água, tendo em conta o O2 consumido na oxidação da matéria orgânica.\n\nPorque é importante medir?\n\nO CQO é um parâmetro indispensável nos estudos de caracterização da qualidade da água e é bastante vantajoso quando utilizado conjuntamente com a CBO5 20⁰C para analisar a biodegradabilidade das águas.\n\nQuais os valores de referência?\n\nVMA: 125 mg / l";
        }
        else if (indexOther == 3)
        {
            textOtherTitle.text = "Fosfatos";
            textOtherDesc.text = "O que é?\n\nO fósforo é um elemento químico que necessita de controlo antes da sua descarga para os vários recursos hídricos, apresentando - se na forma de ortofosfatos, polisfosfatos e fosfatos orgânicos e inorgânicos.Quando é descarregado em excesso nos cursos de águas, favorece o crescimento descontrolado de fitoplâncton e algas, levando à eutrofização do meio.O processo de eutrofização consiste no enriquecimento das águas por nutrientes(essencialmente azoto e fósforo) que levam ao rápido crescimento do fitoplâncton aumentando a turbidez da água.Com o aumento da turbidez, a luz não alcança a profundidade da água, não permitindo a realização da fotossíntese pelas algas mais profundas, que acabam por morrer e entram em decomposição. Com a perda da vegetação aquática submersa desaparece o alimento, o habitat e o oxigénio libertado pela fotossíntese.Resultando na morte da vida aquática devido à drástica redução de oxigénio.\n\nPorque é importante medir?\n\nAs concentrações de fosfato dão indicação do grau da poluição associada a essa massa de água, uma vez que a concentração deste fosfato provem não só da matéria orgânica como da entrada de poluentes químicos num curso de água, como adubos, descargas não tratadas de efluentes industriais e domésticos.\n\nQuais os valores de referência?\n\nNo caso das águas superficiais, os limites estabelecidos para permitir a existência de fauna e flora num ecossistema aquático é: VMA: 1 mg / l P - limites estes descritos no Decreto-lei 236 / 1998.";
        }
        else if (indexOther == 4)
        {
            textOtherTitle.text = "CBO5 (Carência Bioquímica de Oxigénio)";
            textOtherDesc.text = "O que é?\n\nO CBO5 é um parâmetro analítico de qualidade da água que mede indiretamente a quantidade de matéria biodegradável através da quantidade de O2 consumida pelos microrganismos na sua degradação.\n\nPorque é importante medir?\n\nA carência de O2 na água provoca problemas ambientais, entre eles a libertação de gases nocivos à saúde humana e impede a existência de peixes e outros seres aquáticos, que morrem por asfixia.\n\nQuais os valores de referência?\n\nNo caso das águas superficiais, os limites estabelecidos para permitir a existência de fauna e flora num ecossistema aquático é: VMA - 5 mg / l O2 - limites estes descritos no Decreto-lei 236 / 1998.";
        }
        isOpenR = true;
        HideLeftInformation();
        panelCenter.SetActive(false);
        LeanTween.move(rightCanvas, new Vector3(-xValue, 0.0f, 0f), 1f).setCanvasMoveX().setDelay(0.1f);
    }
   
}
