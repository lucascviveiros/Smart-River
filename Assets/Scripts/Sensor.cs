using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor
{
    private int codigo;
    private int humidadeAr;
    private int tempAgua;
    private int tempAmb;
    private float condutividadeEletrica;
    private float ph;
    private DateTime data;
    private List<Sensor> list = null;
    private float[,] mediaGeralMes = new float[5, 12];
    private float[,,] mediaGeralMesAno = new float[5, 12, 12]; //Atributo Meses Anos
    private float[,,,] mediaSemanalMensal = new float[4, 12, 5, 12]; //4 semanas, 12 meses, 5 atributos 12 anos
    private int[] quantPorMes = new int[12];
    private int[,] quantPorMesAno = new int[12,12];
    private int[,,] quantSemanalMensal = new int[4, 12, 12]; //4 semanas 12 meses 12 anos
    List<string> mesesVal = new List<string>() { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
    private int quantAnos = 0;

    //private static Singleton instance
    private static Sensor instance = null;
    
    public static Sensor getInstance()
    {
        if (instance == null)
            instance = new Sensor();

        return instance;
    } 

    //public Singleton List
    public Sensor()
    {
        list = new List<Sensor>();
    }

    public List<Sensor> getArray()
    {
        return list;
    }

    //Adding Singleton to List
    public void AddToArray(Sensor value)
    {
        list.Add(value);
        //Debug.Log("addArray list.count " + list.Count);
        //Debug.Log("value codigo " + value.GetCodigo() + "humid " + value.GetHumidadeAr());
    }

    public void DeleteList()
    {

        for (int i = 0; i < list.Count; i++)
            list.Remove(list[i]);

        list.Clear();
        /*
        //Limpando MediaSemanalMensal
        for (int y = 0; y <= GetQuantAnos(); y++)
        {

            for (int m = 0; m < mesesVal.Count; m++)
            {
                for (int s = 0; s < 4; s++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        mediaSemanalMensal[s, m, k, y] = 0;
                    }
                }
            }
        }

        //Limpando MediaGeralMes
        for (int i = 0; i < mesesVal.Count; i++)
        {
            for (int s = 0; s < 5; s++)
            {
                mediaGeralMes[s, i] = 0;
                //Debug.Log("Mes:" + i + "Atributo" + s + "Valor" + mediaGeralMes[s,i].ToString());
            }            
        }

        //Sabendo a quantidade, ate qual mes
        int ateQualMes = 0;
        for (int i = 0; i < quantPorMes.Length; i++)
        {
            //Debug.Log("q:" + quantPorMes[i]);
            if (quantPorMes[i] != 0)
            {
                ateQualMes++;
            }
        }   

        //Alterar
        //Limpando a quantidadeSemanalMensal

        for (int y = 0; y <= GetQuantAnos(); y++)
        {
            for (int s = 0; s < 4; s++)
            {
                for (int m = 0; m < ateQualMes; m++)
                {
                    quantSemanalMensal[s, m, y] = 0;
                    //Debug.Log("S:" + s + "");
                    Debug.Log("M:" + m + "quant: " + quantSemanalMensal[s, m, y].ToString());
                }
            }
        }


        for (int i = 0; i<12; i++)
        {
            quantPorMes[i] = 0;
        }
        */
        Array.Clear(mediaGeralMesAno, 0, mediaGeralMesAno.Length);
        Array.Clear(mediaSemanalMensal, 0, mediaSemanalMensal.Length);
        Array.Clear(mediaGeralMes, 0, mediaGeralMes.Length);

        Array.Clear(quantSemanalMensal, 0, quantSemanalMensal.Length);
        Array.Clear(quantPorMes, 0, quantPorMes.Length);
        Array.Clear(quantPorMesAno, 0, quantPorMesAno.Length);


    }

    public void ShowAll()
    {
        foreach (Sensor i in list)
        {
            Debug.Log("Sensor: " + i.GetAll().ToString());
        }
    }

    public float[,] GetMediaGeralMes()
    {
        return mediaGeralMes;
    }

    public void SetMediaGeralMes(float[,] mediaGeralMes)
    {
        this.mediaGeralMes = mediaGeralMes;
    }

    /// Coisa nova aq
    public float[,,] GetMediaGeralMesAno()
    {
        return mediaGeralMesAno;
    }

    public void SetMediaGeralMesAno(float[,,] mediaGeralMesAno)
    {
        this.mediaGeralMesAno = mediaGeralMesAno;
    }
    /// **************
    public float[,,,] GetMediaSemanalMensal()
    {
        return mediaSemanalMensal;
    }

    public void SetMediaSemanalMensal(float[,,,] mediaSemanalMensal)
    {
        this.mediaSemanalMensal = mediaSemanalMensal;
    }

    public int[] GetQuantidadeMes()
    {
        return quantPorMes;
    }

    public void SetQuantidadeMes(int[] quantPorMes)
    {
        this.quantPorMes = quantPorMes;
    }

    public int[,,] GetQuantidadeSemanalMensal()
    {
        return quantSemanalMensal;
    }

    public void SetQuantidadeSemanalMensal(int[,,] quantSemanalMensal)
    {
        this.quantSemanalMensal = quantSemanalMensal;
    }
    /// <summary>
    public int[,] GetQuantidadeMensalAnual()
    {
        return quantPorMesAno;
    }

    public void SetQuantidadeMensalAnual(int[,] quantPorMesAno)
    {
        this.quantPorMesAno = quantPorMesAno;
    }
    /// 

    public int GetQuantAnos()
    {
        return quantAnos;
    }

    public void SetQuantAnos(int quantAnos)
    {
        this.quantAnos = quantAnos;
    }


    public void MediaMensalSemanal()
    {
        //Debug.Log("QuantidadeSemanal" +  quantSemanalMensal.Length + "");
        //MediaSemanal mediaSemanalJaneiro = new MediaSemanal("Janeiro");
        int contAnos = 0;

        foreach (Sensor i in list)
        {
            //Debug.Log("ANO: " + i.GetDate().ToString("yyyy"));
            //coisa nova
            contAnos = int.Parse(i.GetDate().ToString("yyyy")) - 2020;
            //Janeiro
            if (i.GetDate().ToString("MM") == mesesVal[0])
            {
                quantPorMes[0]++;
                AddByMonth(i, 0);

                quantPorMesAno[0,contAnos]++;
                //Debug.Log("QuantMesAno: " + quantPorMesAno[0, contAnos] + " contAnos: " + contAnos);
                FilterByWeek(i, 0, contAnos);

                AddByMonthPerYear(i, 0, contAnos);
                
            }
            //Fevereiro
            else if (i.GetDate().ToString("MM") == mesesVal[1])
            {
                quantPorMes[1]++;
                AddByMonth(i, 1);
                quantPorMesAno[1, contAnos]++;
                AddByMonthPerYear(i, 1, contAnos);
                FilterByWeek(i, 1, contAnos);
            }
            //Março
            else if (i.GetDate().ToString("MM") == mesesVal[2])
            {
                quantPorMes[2]++;
                AddByMonth(i, 2);

                quantPorMesAno[2, contAnos]++;
                AddByMonthPerYear(i, 2, contAnos);

                FilterByWeek(i, 2, contAnos);

            }
            //Abril
            else if (i.GetDate().ToString("MM") == mesesVal[3])
            {
                quantPorMes[3]++;
                AddByMonth(i, 3);

                quantPorMesAno[3, contAnos]++;
                AddByMonthPerYear(i, 3, contAnos);
                FilterByWeek(i, 3, contAnos);

            }
            //Maio
            else if (i.GetDate().ToString("MM") == mesesVal[4])
            {
                quantPorMes[4]++;
                AddByMonth(i, 4);
                quantPorMesAno[4, contAnos]++;
                AddByMonthPerYear(i, 4, contAnos);
                FilterByWeek(i, 4, contAnos);

            }
            //Junho
            else if (i.GetDate().ToString("MM") == mesesVal[5])
            {
                quantPorMes[5]++;
                AddByMonth(i, 5);
                quantPorMesAno[5, contAnos]++;
                AddByMonthPerYear(i, 5, contAnos);
                FilterByWeek(i, 5, contAnos);

            }
            //Julho
            else if (i.GetDate().ToString("MM") == mesesVal[6])
            {
                quantPorMes[6]++;
                AddByMonth(i, 6);
                quantPorMesAno[6, contAnos]++;
                AddByMonthPerYear(i, 6, contAnos);
                FilterByWeek(i, 6, contAnos);

            }
            //Agosto
            else if (i.GetDate().ToString("MM") == mesesVal[7])
            {
                quantPorMes[7]++;
                AddByMonth(i, 7);

                quantPorMesAno[7, contAnos]++;
                AddByMonthPerYear(i, 7, contAnos);
                FilterByWeek(i, 7, contAnos);

            }
            //Setembro
            else if (i.GetDate().ToString("MM") == mesesVal[8])
            {
                quantPorMes[8]++;
                AddByMonth(i, 8);

                quantPorMesAno[8, contAnos]++;
                AddByMonthPerYear(i, 8, contAnos);
                FilterByWeek(i, 8, contAnos);

            }
            //Outubro
            else if (i.GetDate().ToString("MM") == mesesVal[9])
            {
                quantPorMes[9]++;
                AddByMonth(i, 9);

                quantPorMesAno[9, contAnos]++;
                AddByMonthPerYear(i, 9, contAnos);
                FilterByWeek(i, 9, contAnos);

            }
            //Novembro
            else if (i.GetDate().ToString("MM") == mesesVal[10])
            {
                quantPorMes[10]++;
                AddByMonth(i, 10);
                quantPorMesAno[10, contAnos]++;
                AddByMonthPerYear(i, 10, contAnos);
                FilterByWeek(i, 10, contAnos);

            }
            //Dezembro
            else if (i.GetDate().ToString("MM") == mesesVal[11])
            {
                quantPorMes[11]++;
                AddByMonth(i, 11);

                quantPorMesAno[11, contAnos]++;
                AddByMonthPerYear(i, 11, contAnos);
                FilterByWeek(i, 11, contAnos);

            }
        }

        //Passando quantidade de anos
        getInstance().SetQuantAnos(contAnos);

        //Media Mensal Simples apenas 1 ano
        for (int i = 0; i < mesesVal.Count; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (quantPorMes[i] != 0)
                {
                    mediaGeralMes[j, i] = mediaGeralMes[j, i] / quantPorMes[i];
                }
            }
        }

        int[,] ateQualMesAno;
        //Quantos Meses tem nesse ano
        for(int y = 0; y <= contAnos; y++)
        {
            for (int m = 0; m < mesesVal.Count; m++)
            {
                if(quantSemanalMensal[0, m, y] != 0)
                {

                }
            }
        }

        //***
        for (int y = 0; y <= contAnos; y++) 
        {
            for (int m = 0; m < mesesVal.Count; m++)
            {
                for (int s = 0; s <= 3; s++)
                {
                    for (int a = 0; a <= 4; a++)
                    {
                        if (quantSemanalMensal[s, m, y] != 0)
                        {
                            mediaSemanalMensal[s, m, a, y] = mediaSemanalMensal[s, m, a, y] / quantSemanalMensal[s, m, y];
                        }//Debug.Log("QuantSemanalMensal: " + quantSemanalMensal[s, m, y]);
                        //Debug.Log("MediaSemanalMensal: " + mediaSemanalMensal[s, m, a, y]);
                    }
                }
            } 
        }

        ////Media Mensal Anual

        for (int y = 0; y <= contAnos; y++)
        {
            for (int m = 0; m < mesesVal.Count; m++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (quantPorMesAno[m, y] != 0)
                    {
                        mediaGeralMesAno[j, m, y] = mediaGeralMesAno[j, m, y] / quantPorMesAno[m, y];
                    }
                }
            }
        }


        getInstance().SetQuantidadeMes(quantPorMes);
        getInstance().SetMediaGeralMes(mediaGeralMes);

        //Coisa nova aq
        getInstance().SetQuantidadeSemanalMensal(quantSemanalMensal);
        getInstance().SetMediaSemanalMensal(mediaSemanalMensal);

        //
        getInstance().SetQuantidadeMensalAnual(quantPorMesAno);
        getInstance().SetMediaGeralMesAno(mediaGeralMesAno);
        //mediaSemanalJaneiro.MediaPorSemana();
    }

    public void AddByMonthPerYear(Sensor i, int month, int year)
    {
        mediaGeralMesAno[0, month, year] += i.GetHumidadeAr();
        mediaGeralMesAno[1, month, year] += i.GetTempAmb();
        mediaGeralMesAno[2, month, year] += i.GetTempAgua();
        mediaGeralMesAno[3, month, year] += i.GetCondutividade();
        mediaGeralMesAno[4, month, year] += i.GetPH();

    }

    public void AddByMonth(Sensor i, int month)
    {
        mediaGeralMes[0, month] += i.GetHumidadeAr();
        mediaGeralMes[1, month] += i.GetTempAmb();
        mediaGeralMes[2, month] += i.GetTempAgua();
        mediaGeralMes[3, month] += i.GetCondutividade();
        mediaGeralMes[4, month] += i.GetPH();
    }

    public void AddByWeek(Sensor i, int semana, int mes, int ano)
    {
        //4semanas  12meses  5atributos
        mediaSemanalMensal[semana, mes, 0, ano] += i.GetHumidadeAr();
        mediaSemanalMensal[semana, mes, 1, ano] += i.GetTempAmb();
        mediaSemanalMensal[semana, mes, 2, ano] += i.GetTempAgua();
        mediaSemanalMensal[semana, mes, 3, ano] += i.GetCondutividade();
        mediaSemanalMensal[semana, mes, 4, ano] += i.GetPH();
    }

    public void FilterByWeek(Sensor i, int mes, int ano)
    {
        if (int.Parse(i.GetDate().ToString("dd")) <= 7)
        {
            quantSemanalMensal[0, mes, ano]++;  //4 12
            //Debug.Log("FilterByWeek: " + quantSemanalMensal[0, mes, ano]);
            AddByWeek(i, 0, mes, ano);
        }
        else if (int.Parse(i.GetDate().ToString("dd")) > 7 && (int.Parse(i.GetDate().ToString("dd")) <= 14))
        {
            quantSemanalMensal[1, mes, ano]++;
            AddByWeek(i, 1, mes, ano);
        }
        else if (int.Parse(i.GetDate().ToString("dd")) > 14 && (int.Parse(i.GetDate().ToString("dd")) <= 21))
        {
            quantSemanalMensal[2, mes, ano]++;
            AddByWeek(i, 2, mes, ano);
        }
        else if (int.Parse(i.GetDate().ToString("dd")) > 21 && (int.Parse(i.GetDate().ToString("dd")) <= 28))
        {
            quantSemanalMensal[3, mes, ano]++;
            AddByWeek(i, 3, mes, ano);
        }
    }

    public Sensor(int codigo, int humidadeAr, int tempAgua, int tempAmb, int condutividadeEletrica, float ph, DateTime data)
    {
        this.codigo = codigo;
        this.humidadeAr = humidadeAr;
        this.tempAgua = tempAgua;
        this.condutividadeEletrica = condutividadeEletrica;
        this.ph = ph;
        this.data = data;
    }  
    
    public int GetHumidadeAr()
    {
        return humidadeAr;
    }

    public void SetHumidadeAr(int humidadeAr)
    {
        this.humidadeAr = humidadeAr;
    }

    public int GetTempAgua()
    {
        return tempAgua;
    }

    public void SetTempAgua(int tempAgua)
    {
        this.tempAgua = tempAgua;
    }

    public int GetTempAmb()
    {
        return tempAmb;
    }

    public void SetTempAmb(int tempAmb)
    {
        this.tempAmb = tempAmb;
    }
    public float GetCondutividade()
    {
        return condutividadeEletrica;
    }

    public void SetCondutividade(float condutividadeEletrica)
    {
        this.condutividadeEletrica = condutividadeEletrica;
    }

    public float GetPH()
    {
        return ph;
    }

    public void SetPH(float ph)
    {
        this.ph = ph;
    }

    public DateTime GetDate()
    {
        return data;
    }

    public void SetDate(DateTime data)
    {
        this.data = data;
    }

    public int GetCodigo()
    {
        return codigo;
    }

    public void setCodigo(int cod)
    {
        codigo = cod;
    } 
    
    public string GetAll()
    {
            string All = "Cod: " + getInstance().codigo + "|TempAgua: " + getInstance().tempAgua + "|TempAmb: " + getInstance().tempAmb + "|CondutividadeE:" + getInstance().condutividadeEletrica + "|Ph: " + getInstance().ph + "|Data: " + getInstance().data.ToString("dd/MM/yyyy") ;
       //     Debug.Log(":" + All);
            return All;
    } 

}

