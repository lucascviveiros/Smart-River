using Graph.VittorCloud;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BarGraph.VittorCloud
{
    public class BarGraphManager : GraphBox
    {
        #region PublicVariables
        
        public Action GraphUpdated;
        #endregion

        #region PrivateVariables
        GameObject barRef;
        BarGraphGenerator graphGenerator;
        #endregion

        #region UnityCallBacks
        public void Awake()
        {
            base.Awake();

        }
        public void Start()
        {
            if (barRef == null)
                Debug.LogError("Bar Prefabe is Empty!! Please assign the bar prefabe to GraphBox.");


            graphGenerator = GetComponentInParent<BarGraphGenerator>();
            
        }
        #endregion

        #region BarGraph Custom methods


        public void SetBarRef(GameObject bar) {

            barRef = bar;

        }
        #region AnimationTypeOne
        public IEnumerator GenerateGraphBarWithAnimTypeOne(int xIndex, int zIndex, float yValue, float scaleFactor, float animSpeed, int ymin, int xMax, Color barColor)
        {
            if (barRef == null)
                yield return null;

            barParent.transform.localScale = Vector3.one;
            //
            GameObject bar = GameObject.Instantiate(barRef, transform.position, transform.rotation);
            bar.transform.parent = ListOfGroups[zIndex].transform;
            //Vector3 pos = new Vector3(ListOfXPoint[xIndex].transform.position.x, 0, ListOfZPoints[zIndex].transform.position.z);
            // Debug.Log("Yes I am calling -----");
            Vector3 pos = new Vector3(ListOfXPoint[xIndex].transform.localPosition.x, 0, 0);
            bar.transform.localPosition = pos;
            bar.transform.localScale = bar.transform.localScale * graphScaleFactor;


            BarProperty barProperty = bar.GetComponent<BarProperty>();
            SetBarProperties(barProperty);
            barProperty.SetBarColor(barColor);

            ListOfGroups[zIndex].ListOfBar.Add(bar);
            float yscale = (yValue - ymin) * scaleFactor;
            yield return StartCoroutine(AnimateBarsWithAnimTypeOne(barProperty, yscale, yValue, animSpeed, false));


            yield return new WaitForEndOfFrame();

        }
        public IEnumerator GenerateGraphBarWithAnimTypeOne(int xIndex, int zIndex, float yValue, float scaleFactor, float animSpeed, int ymin, int xMax, Material barMaterial)
        {


            if (barRef == null)
                yield return null;

            barParent.transform.localScale = Vector3.one;
            GameObject bar = GameObject.Instantiate(barRef, transform.position, transform.rotation);
            bar.transform.parent = ListOfGroups[zIndex].transform;
            //Vector3 pos = new Vector3(ListOfXPoint[xIndex].transform.position.x, 0, ListOfZPoints[zIndex].transform.position.z);
            // Debug.Log("Yes I am calling -----");
            Vector3 pos = new Vector3(ListOfXPoint[xIndex].transform.localPosition.x, 0, 0);
            bar.transform.localPosition = pos;
            bar.transform.localScale = bar.transform.localScale * graphScaleFactor;




            ListOfGroups[zIndex].ListOfBar.Add(bar);

            BarProperty barProperty = bar.GetComponent<BarProperty>();
            SetBarProperties(barProperty);
            if (barMaterial != null)
                barProperty.SetBarMat(barMaterial);

            float yscale = (yValue - ymin) * scaleFactor;
            yield return StartCoroutine(AnimateBarsWithAnimTypeOne(bar.GetComponent<BarProperty>(), yscale, yValue, animSpeed, false));

            yield return new WaitForEndOfFrame();

        }
        public IEnumerator AnimateBarsWithAnimTypeOne(BarProperty bar, float barScale, float yValue, float animSpeed, bool isUpdating)
        {
            while (true)
            {



                Vector3 targetScale = new Vector3(bar.transform.localScale.x, barScale, bar.transform.localScale.z);
                BarProperty barProperty = bar.GetComponent<BarProperty>();

                if (bar.transform.localScale.y > targetScale.y)
                {
                    Vector3 scale = bar.transform.localScale - new Vector3(0, Time.deltaTime * animSpeed, 0);
                    scale.y = Mathf.Clamp(scale.y, targetScale.y, yValue);
                    Debug.Log($"y scale {scale.y} traget scale {targetScale.y} yvalue {yValue}");
                    bar.transform.localScale = scale;

                    if (bar.transform.localScale.y <= targetScale.y)
                    {
                        barProperty.SetBarLabelVisible(yValue.ToString(), graphScaleFactor);
                        break;
                    }
                }
                else
                {
                    Vector3 scale = bar.transform.localScale + new Vector3(0, Time.deltaTime * animSpeed, 0);
                    scale.y = Mathf.Clamp(scale.y, 0, targetScale.y);

                    Debug.Log($"y scale {scale.y} traget scale {targetScale.y} yvalue {yValue}");
                    bar.transform.localScale = scale;
                    if (bar.transform.localScale.y >= targetScale.y)
                    {
                        barProperty.SetBarLabelVisible(yValue.ToString(), graphScaleFactor);
                        break;
                    }
                }

                yield return new WaitForEndOfFrame();
            }

            if (isUpdating)
                GraphUpdated();

            yield return new WaitForEndOfFrame();

        }
        #endregion

        #region AnimationTypeTwo

        //Animation All Together 
        public void GenerateBarWithAnimTypeTwo(int xIndex, int zIndex, float yValue, float scaleFactor, float animSpeed, int ymin, int xMax, Color barColor)
        {
            if (barRef == null)
                return;


            GameObject bar = GameObject.Instantiate(barRef, transform.position, transform.rotation);
            //bar.transform.parent = ListOfGroups[zIndex].transform;
            bar.transform.SetParent(ListOfGroups[zIndex].transform);


            //Vector3 pos = new Vector3(ListOfXPoint[xIndex].transform.position.x, 0, ListOfZPoints[zIndex].transform.position.z);
            // Debug.Log("Yes I am calling -----");
            Vector3 pos = new Vector3(ListOfXPoint[xIndex].transform.localPosition.x, 0, 0);
            bar.transform.localPosition = pos;
            bar.transform.localScale = bar.transform.localScale * graphScaleFactor;

            BarProperty barProperty = bar.GetComponent<BarProperty>();

            SetBarProperties(barProperty);
            barProperty.SetBarColor(barColor);
            barProperty.SetBarLabel(yValue.ToString(), graphScaleFactor);

            /*travando fator de escala da barra
            if (float.IsInfinity(scaleFactor))
            {
                scaleFactor = 1.5f;
            }
            if(Math.Round(scaleFactor,1) <= 0.33) 
            {
                scaleFactor = 0.6f;
            }
            */
            float yscale = (yValue - ymin) * scaleFactor; //retornando NaN quando escala e infinita
            
            if (float.IsNaN(yscale))
            {
                Debug.Log("caiu no Is Nan");
                scaleFactor = 1;
                ymin = int.Parse(yValue.ToString()) - 5;
                yscale = (yValue - ymin) * scaleFactor;  
            }
            else if(scaleFactor > 9)
            {
                scaleFactor = 1;
                //ymin = int.Parse(yValue.ToString()) - 5;
                yscale = (yValue - ymin) * scaleFactor;
            }

            Debug.Log("yValue " + yValue.ToString() + " ymin: " + ymin + " scalefactor: " + scaleFactor);

            bar.transform.localScale = new Vector3(bar.transform.localScale.x, yscale, bar.transform.localScale.z);
            ///Acontecendo algo aqui em cima

            ListOfGroups[zIndex].ListOfBar.Add(bar);

        }
        /*
        public void GenerateBarWithAnimTypeTwo(int xIndex, int zIndex, float yValue, float scaleFactor, float animSpeed, int ymin, int xMax, Material barMaterial)
        {
            if (barRef == null)
                return;

            GameObject bar = GameObject.Instantiate(barRef, transform.position, transform.rotation);
            bar.transform.parent = ListOfGroups[zIndex].transform;
            //Vector3 pos = new Vector3(ListOfXPoint[xIndex].transform.position.x, 0, ListOfZPoints[zIndex].transform.position.z);
            // Debug.Log("Yes I am calling -----");
            Vector3 pos = new Vector3(ListOfXPoint[xIndex].transform.localPosition.x, 0, 0);
            bar.transform.localPosition = pos;
            bar.transform.localScale = bar.transform.localScale * graphScaleFactor;

            BarProperty barProperty = bar.GetComponent<BarProperty>();
            SetBarProperties(barProperty);
            if (barMaterial != null)
                barProperty.SetBarMat(barMaterial);
            barProperty.SetBarLabel(yValue.ToString(), graphScaleFactor);

            float yscale = (yValue - ymin) * scaleFactor;
            bar.transform.localScale = new Vector3(bar.transform.localScale.x, yscale, bar.transform.localScale.z);

            ListOfGroups[zIndex].ListOfBar.Add(bar);

        } */
        public IEnumerator AnimateBarsWithAnimTypeTwo(float animSpeed)
        {
            barParent.transform.localScale = new Vector3(1, 0, 1);
            while (true)
            {

                /*
                 n: The object of type 'GameObject' has been destroyed but you are still trying to access it.
                    Your script should either check if it is null or you should not destroy the object.
                        BarGraph.VittorCloud.BarGraphManager+<AnimateBarsWithAnimTypeTwo>d__10.MoveNext () (at Assets/VC/3D Interactive Bar Chart/2.Scripts/BarGraph/BarGraphManager.cs:248)
                        UnityEngine.SetupCoroutine.InvokeMoveNext (System.Collections.IEnumerator enumerator, System.IntPtr returnValueAddress) (at <23a7799da2e941b88c6db790c607d655>:0)
                        UnityEngine.GUIUtility:ProcessEvent(Int32, IntPtr)
                 */
                //Deu bug aqui
                Vector3 scale = barParent.transform.localScale + new Vector3(0, Time.deltaTime * animSpeed, 0);
                scale.y = Mathf.Clamp(scale.y, 0, 1);
                barParent.transform.localScale = scale;

                if (barParent.transform.localScale.y >= 1)
                {
                    foreach (BarProperty bar in barParent.GetComponentsInChildren<BarProperty>())

                        bar.GetComponent<BarProperty>().SetLabelEnabel();

                    break;
                }

                yield return new WaitForEndOfFrame();
            }
            GraphUpdated();
            yield return new WaitForEndOfFrame();

        }
        #endregion

        #region AnimtionTypeThree
        public IEnumerator GenerateGraphBarWithAnimTypeThree(int xIndex, int zIndex, float yValue, float scaleFactor, float animSpeed, int ymin, int xMax, Color barColor, Color barStartColor)
        {
            if (barRef == null)
                yield return null;

            barParent.transform.localScale = Vector3.one;



            GameObject bar = GameObject.Instantiate(barRef, transform.position, transform.rotation);
            bar.transform.parent = ListOfGroups[zIndex].transform;
            // Vector3 pos = new Vector3(ListOfXPoint[xIndex].transform.localPosition.x, 0, ListOfZPoints[zIndex].transform.localPosition.z);
            Vector3 pos = new Vector3(ListOfXPoint[xIndex].transform.localPosition.x, 0, 0);
            bar.transform.localPosition = pos;
            bar.transform.localScale = bar.transform.localScale * graphScaleFactor;

            BarProperty barProperty = bar.GetComponent<BarProperty>();

            SetBarProperties(barProperty);
            barProperty.SetBarColor(barStartColor);

            ListOfGroups[zIndex].ListOfBar.Add(bar);
            float yscale = (yValue - ymin) * scaleFactor;
            yield return StartCoroutine(AnimateBarsWithAnimTypeThree(bar.GetComponent<BarProperty>(), yscale, yValue, animSpeed, false, barColor, barStartColor));


            yield return new WaitForEndOfFrame();

        }
        public IEnumerator AnimateBarsWithAnimTypeThree(BarProperty bar, float barScale, float yValue, float animSpeed, bool isUpdating, Color barColor, Color barStartColor)
        {
            while (true)
            {

                BarProperty barProperty = bar.GetComponent<BarProperty>();
                Color barCol = Color.Lerp(barProperty.GetBarColor(), barColor, Time.deltaTime * animSpeed * 0.4f);
                barProperty.SetBarColor(barCol);
                Vector3 targetScale = new Vector3(bar.transform.localScale.x, barScale, bar.transform.localScale.z);
                if (bar.transform.localScale.y > targetScale.y)
                {
                    Vector3 scale = bar.transform.localScale - new Vector3(0, Time.deltaTime * animSpeed, 0);
                    scale.y = Mathf.Clamp(scale.y, targetScale.y, yValue);
                    bar.transform.localScale = scale;



                    if (bar.transform.localScale.y <= targetScale.y)
                    {
                        barProperty.SetBarLabelVisible(yValue.ToString(), graphScaleFactor);
                        barProperty.SetBarColor(barColor);
                        break;
                    }
                }
                else
                {
                    Vector3 scale = bar.transform.localScale + new Vector3(0, Time.deltaTime * animSpeed, 0);
                    scale.y = Mathf.Clamp(scale.y, 0, targetScale.y);
                    bar.transform.localScale = scale;


                    if (bar.transform.localScale.y >= targetScale.y)
                    {
                        barProperty.SetBarLabelVisible(yValue.ToString(), graphScaleFactor);
                        barProperty.SetBarColor(barColor);
                        break;
                    }
                }



                yield return new WaitForEndOfFrame();
            }

            if (isUpdating)
                GraphUpdated();

            yield return new WaitForEndOfFrame();

        }
        #endregion

        #region UpdateGraph
        public void UpdateBarHeight(int xIndex, int zIndex, float yValue, float scaleFactor, float animSpeed, int ymin)
        {


            GameObject bar = ListOfGroups[zIndex].ListOfBar[xIndex];
            float yscale = (yValue - ymin) * scaleFactor;
            BarProperty barProperty = bar.GetComponent<BarProperty>();
            barProperty.LabelContainer.SetActive(false);
            StartCoroutine(AnimateBarsWithAnimTypeOne(barProperty, yscale, yValue, animSpeed, true));



        }
        public void UpdateBarHeight(int xIndex, int zIndex, float yValue, float scaleFactor, float animSpeed, int ymin, Color barColor)
        {


            GameObject bar = ListOfGroups[zIndex].ListOfBar[xIndex];
            float yscale = (yValue - ymin) * scaleFactor;
            BarProperty barProperty = bar.GetComponent<BarProperty>();
            barProperty.LabelContainer.SetActive(false);
            barProperty.SetBarColor(barColor);
            StartCoroutine(AnimateBarsWithAnimTypeOne(barProperty, yscale, yValue, animSpeed, true));



        }
        public void UpdateBarHeight(int xIndex, int zIndex, float yValue, float scaleFactor, float animSpeed, int ymin, Color barColor, Color barStartColor)
        {


            GameObject bar = ListOfGroups[zIndex].ListOfBar[xIndex];
            BarProperty barProperty = bar.GetComponent<BarProperty>();
            float yscale = (yValue - ymin) * scaleFactor;
            barProperty.LabelContainer.SetActive(false);
            StartCoroutine(AnimateBarsWithAnimTypeThree(barProperty, yscale, yValue, animSpeed, true, barColor, barStartColor));



        }
        #endregion



        public void RemoveAndShiftXpoints(string xValue)
        {


            for (int i = 0; i < ListOfXPoint.Count - 1; i++)
            {
                Debug.Log("Cont " + i + " " + ListOfXPoint[i].labelText + " " + ListOfXPoint[i + 1].labelText);

                ListOfXPoint[i].labelText = ListOfXPoint[i + 1].labelText;
            }

            ListOfXPoint[ListOfXPoint.Count - 1].labelText = xValue.ToString();

        }
        public void SetBarProperties(BarProperty barProperty)
        {
            barProperty.barClickEvents.PointerDownOnBar += OnBarPointerDown;
            barProperty.barClickEvents.PointerUpOnBar += OnBarPointerUp;
            barProperty.barClickEvents.PointerEnterOnBar += OnBarPointerEnter;
            barProperty.barClickEvents.PointerExitOnBar += OnBarPointerExit;

        }
        #endregion

        #region ClickEvents

        public void OnBarPointerDown(GameObject Clickedbar)
        {
            if (graphGenerator != null)
                graphGenerator.OnBarPointerDown.Invoke(Clickedbar);

        }
        public void OnBarPointerUp(GameObject Clickedbar)
        {
            if (graphGenerator != null)
                graphGenerator.OnBarPointerUp.Invoke(Clickedbar);
        }
        public void OnBarPointerEnter(GameObject Clickedbar)
        {
            if (graphGenerator != null)
                graphGenerator.OnBarHoverEnter.Invoke(Clickedbar);
        }
        public void OnBarPointerExit(GameObject Clickedbar)
        {
            if (graphGenerator != null)
                graphGenerator.OnBarHoverExit.Invoke(Clickedbar);
        }
        #endregion
    }
}
