using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class StreamingText : MonoBehaviour
{
    private string filePath;
    private DateTime dateTime;
    public TextMeshProUGUI debugText;
    public Button btnUpdate;
    private string year;
    public SmartRiverGraph smartRiver;
    public TextMeshProUGUI yearTxT;

    private void Awake()
    {
        filePath = @"C:\Users\Utilizador\Desktop\Smart-River_Data\Resources\DadosCasadaSeda.txt"; //ou .log

        if (System.IO.File.Exists(filePath))
        {
            StreamingFile(filePath);
            debugText.text = "";
        }
        else
        {
            debugText.text = "Erro com Arquivo de leitura";
        }

        btnUpdate.onClick.AddListener(OnButtonUpdateClick);

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            Debug.Log("Enter pressed");
            
            foreach(Sensor s in Sensor.getInstance().getArray())
            {
                Debug.Log("Sensores) cod: " + s.GetCodigo() +" Humida: "+ s.GetHumidadeAr() + " DATE: " + s.GetDate());
            }
        }
    }

    public void OnButtonUpdateClick() 
    {
        DestroyBefore();
 
        if (System.IO.File.Exists(filePath))
        {
            debugText.text = "";
            StreamingFile(filePath);
        }
        else
        {
            debugText.text = "Erro com Arquivo de leitura";
        }
    }

    public void DestroyBefore()
    {
        foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
        {
            if (obj.name == "GraphBox(Clone)")
            {
                Destroy(GameObject.Find("GraphBox(Clone)"));
            }
        }
    }

    public void StreamingFile(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        string[] valores = new string[6];
        string valor = "";

        //Debug.Log("Lines: " + lines.Length);

        for (int k = 0; k < lines.Length; k++)
        {
            //Debug.Log("Value: " + lines[k].Length);
            if (lines[k].Contains("null") || lines[k].Contains(" ") || lines[k].StartsWith(" ") || lines[k].Length < 49)
            {
                Debug.Log("Line contain Null [jump]");
            }

            else
            {
                //Debug.Log("Number of Arguments Between ',' : " + lines[0].Split(',').Count()); 
                for (int coluna = 0; coluna < lines[k].Split(',').Count(); coluna++)
                {
                    //Get Humidade 
                    if (coluna == 0)
                    {
                        char c1 = lines[k][1];
                        char c2 = lines[k][2];
                        string humidade = c1.ToString() + c2.ToString();

                        int number;
                        if (int.TryParse(humidade, out number))
                        {
                            valores[coluna] = number.ToString();
                        }                       
                    }
                                     

                    //Get Date
                    else if (coluna == 5)
                    {
                        string value = lines[k].Split(',')[coluna];
                        char[] delimiter1 = new char[] { '"', 'T' };
                        string[] array2 = value.Split(delimiter1, StringSplitOptions.RemoveEmptyEntries);
                    
                        //Debug.Log("Posicao 0: " + array2[0]);
                        dateTime = DateTime.Parse(array2[0]);
                        //print("Datetime: " + dateTime.ToString("dd/MM/yyyy"));
                    } 
                    //Get Other values
                    else
                    {
                        valor = (lines[k].Split(',')[coluna]);
                        valores[coluna] = valor;
                    }

                    ///
                    //Verificando se tem valores 0
                    for (int v = 0; v < valores.Length; v++)
                    {
                        if (valores[v] == "0")
                        {
                            int linha = k + 1;
                            int column = v + 1;
                            string text = "Contem '0' na linha [" + linha + "] coluna [" + column + "]";
                            //debugText.text = " \n" + text;
                            Debug.Log(text);
                        }
                        else
                        {
                            debugText.text = "";
                        }
                    }
                }
                //year = yearTxT.text.ToString();                   
                
                Sensor sensor = new Sensor();                            
                sensor.setCodigo(k);

                //Debug.Log("Antes de Enviar: " + valores[0].ToString() + " "  + dateTime.ToString("dd/MM/yyyy"));
                sensor.SetHumidadeAr(int.Parse(valores[0]));
                sensor.SetTempAgua(int.Parse(valores[1]));
                sensor.SetTempAmb(int.Parse(valores[2]));
                sensor.SetCondutividade(float.Parse(valores[3]));
                sensor.SetPH(float.Parse(valores[4]));
                sensor.SetDate(dateTime);
                Sensor.getInstance().AddToArray(sensor);

            }
        }

        year = yearTxT.text.ToString();
        Sensor.getInstance().MediaMensalSemanal();
    }

}


