/*
 * Original Touch Script Author:Valentin Simonov / http://va.lent.in/
 * Total Touch Demo Author: Tristan VanFossen
 */

using System.Collections.Generic;
using TouchScript.Utils;
using UnityEngine;

namespace TouchScript.Behaviors.Visualizer
{
    public class TouchVisualizer : MonoBehaviour
    {
        #region Original Touch Script
        #region Public properties

        /// <summary>
        /// Gets or sets touch UI element prefab which represents a touch on screen.
        /// </summary>
        /// <value> A prefab with a script derived from TouchProxyBase. </value>
        public TouchProxyBase TouchProxy
        {
            get { return touchProxy; }
            set
            {
                touchProxy = value;
                updateDefaultSize();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether touch id text should be displayed on screen.
        /// </summary>
        /// <value> <c>true</c> if touch id text should be displayed on screen; otherwise, <c>false</c>. </value>
        public bool ShowTouchId
        {
            get { return showTouchId; }
            set { showTouchId = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether touch tags text should be displayed on screen.
        /// </summary>
        /// <value> <c>true</c> if touch tags text should be displayed on screen; otherwise, <c>false</c>. </value>
        public bool ShowTags
        {
            get { return showTags; }
            set { showTags = value; }
        }

        /// <summary>
        /// Gets or sets whether <see cref="TouchVisualizer"/> is using DPI to scale touch cursors.
        /// </summary>
        /// <value> <c>true</c> if DPI value is used; otherwise, <c>false</c>. </value>
        public bool UseDPI
        {
            get { return useDPI; }
            set { useDPI = value; }
        }

        /// <summary>
        /// Gets or sets the size of touch cursors in cm. This value is only used when <see cref="UseDPI"/> is set to <c>true</c>.
        /// </summary>
        /// <value> The size of touch cursors in cm. </value>
        public float TouchSize
        {
            get { return touchSize; }
            set { touchSize = value; }
        }

        #endregion

        #region Private variables

        [SerializeField]
        private TouchProxyBase touchProxy;

        [SerializeField]
        private bool showTouchId = true;

        [SerializeField]
        private bool showTags = false;

        [SerializeField]
        private bool useDPI = true;

        [SerializeField]
        private float touchSize = 1f;

        private int defaultSize = 64;
        private RectTransform rect;
        private ObjectPool<TouchProxyBase> pool;
        private Dictionary<int, TouchProxyBase> proxies = new Dictionary<int, TouchProxyBase>(10);

        #endregion

        #region Unity methods

        private void Awake()
        {
            pool = new ObjectPool<TouchProxyBase>(10, instantiateProxy, null, clearProxy);
            rect = transform as RectTransform;
            if (rect == null)
            {
                Debug.LogError("TouchVisualizer must be on an UI element!");
                enabled = false;
            }
            updateDefaultSize();
        }

        private void OnEnable()
        {
            if (TouchManager.Instance != null)
            {
                TouchManager.Instance.TouchesBegan += touchesBeganHandler;
                TouchManager.Instance.TouchesEnded += touchesEndedHandler;
                TouchManager.Instance.TouchesMoved += touchesMovedHandler;
            }
        }

        private void OnDisable()
        {
            if (TouchManager.Instance != null)
            {
                TouchManager.Instance.TouchesBegan -= touchesBeganHandler;
                TouchManager.Instance.TouchesEnded -= touchesEndedHandler;
                TouchManager.Instance.TouchesMoved -= touchesMovedHandler;
            }
        }

        #endregion

        #region Private functions

        private TouchProxyBase instantiateProxy()
        {
            return Instantiate(touchProxy);
        }

        private void clearProxy(TouchProxyBase proxy)
        {
            proxy.Hide();
        }

        private int getTouchSize()
        {
            if (useDPI) return (int) (touchSize * TouchManager.Instance.DotsPerCentimeter);
            return defaultSize;
        }

        private void updateDefaultSize()
        {
            if (touchProxy != null)
            {
                var rt = touchProxy.GetComponent<RectTransform>();
                if (rt) defaultSize = (int) rt.sizeDelta.x;
            }
        }

        #endregion
        #endregion

        #region Total Touch Demo
        #region Total Touch Variables

        Vector2 startPos1;
        Vector2 startPos2;
        Vector2 startPos3;
        Vector2 startPos4;

        Vector2 curPos1;
        Vector2 curPos2;
        Vector2 curPos3;
        Vector2 curPos4;

        char dir1, dir2, dir3, dir4;
        double calc1, calc2, calc3, calc4;

        char[] dirArray1 = new char[4];
        char[] dirArray2 = new char[4];
        char[] dirArray3 = new char[4];
        char[] dirArray4 = new char[4];

        int touchCount;

        int testCount;

        int tickCount;

        float timer1;

        string finGesture="SX";
        string stringToEdit = "Adafruit EZ-Link 9545";

        bool startConnection = true;
        string[] gestureList = new string[4];


        #endregion

        #region TotalTouch Demo
        void Update()
        {
           
                if ((Mathf.Sqrt((curPos1.y - startPos1.y) * (curPos1.y - startPos1.y) + (curPos1.x - startPos1.x) * (curPos1.x - startPos1.x))) > (Screen.height * Screen.width * 0.000025) && timer1 + 0.050 < Time.time)
                {
                    timer1 = Time.time;
                    testCount = 0;

                    dirArray1[3] = dirArray1[2];
                    dirArray1[2] = dirArray1[1];
                    dirArray1[1] = dirArray1[0];

                    dirArray2[3] = dirArray2[2];
                    dirArray2[2] = dirArray2[1];
                    dirArray2[1] = dirArray2[0];

                    dirArray3[3] = dirArray3[2];
                    dirArray3[2] = dirArray3[1];
                    dirArray3[1] = dirArray3[0];

                    dirArray4[3] = dirArray4[2];
                    dirArray4[2] = dirArray4[1];
                    dirArray4[1] = dirArray4[0];


                    #region Calculations
                    if (touchCount > 0)
                    {
                        calc1 = Mathf.Atan2((curPos1.y - startPos1.y), curPos1.x - startPos1.x) * 57.2958;

                        if (calc1 < -157.5 || calc1 >= 157.5)
                        {
                            dirArray1[testCount] = 'A';
                            dir1 = 'A';
                        }
                        else if (calc1 < 157.5 && calc1 >= 112.5)
                        {
                            dirArray1[testCount] = 'B';
                            dir1 = 'B';
                        }
                        else if (calc1 < 112.5 && calc1 >= 67.5)
                        {
                            dirArray1[testCount] = 'C';
                            dir1 = 'C';
                        }
                        else if (calc1 < 67.5 && calc1 >= 22.5)
                        {
                            dirArray1[testCount] = 'D';
                            dir1 = 'D';
                        }
                        else if (calc1 < 22.5 && calc1 >= -22.5)
                        {
                            dirArray1[testCount] = 'E';
                            dir1 = 'E';
                        }
                        else if (calc1 < -22.5 && calc1 >= -67.5)
                        {
                            dirArray1[testCount] = 'F';
                            dir1 = 'F';
                        }
                        else if (calc1 < -67.5 && calc1 >= -112.5)
                        {
                            dirArray1[testCount] = 'G';
                            dir1 = 'G';
                        }
                        else if (calc1 < -112.5 && calc1 > -157.5)
                        {
                            dirArray1[testCount] = 'H';
                            dir1 = 'H';
                        }

                    }
                    if (touchCount > 1)
                    {
                        calc2 = Mathf.Atan2((curPos2.y - startPos2.y), curPos2.x - startPos2.x) * 57.2958;

                        if (calc2 < -157.5 || calc2 >= 157.5)
                        {
                            dirArray2[testCount] = 'A';
                            dir2 = 'A';
                        }
                        else if (calc2 < 157.5 && calc2 >= 112.5)
                        {
                            dirArray2[testCount] = 'B';
                            dir2 = 'B';
                        }
                        else if (calc2 < 112.5 && calc2 >= 67.5)
                        {
                            dirArray2[testCount] = 'C';
                            dir2 = 'C';
                        }
                        else if (calc2 < 67.5 && calc2 >= 22.5)
                        {
                            dirArray2[testCount] = 'D';
                            dir2 = 'D';
                        }
                        else if (calc2 < 22.5 && calc2 >= -22.5)
                        {
                            dirArray2[testCount] = 'E';
                            dir2 = 'E';
                        }
                        else if (calc2 < -22.5 && calc2 >= -67.5)
                        {
                            dirArray2[testCount] = 'F';
                            dir2 = 'F';
                        }
                        else if (calc2 < -67.5 && calc2 >= -112.5)
                        {
                            dirArray2[testCount] = 'G';
                            dir2 = 'G';
                        }
                        else if (calc2 < -112.5 && calc2 > -157.5)
                        {
                            dirArray2[testCount] = 'H';
                            dir2 = 'H';
                        }
                    }
                    if (touchCount > 2)
                    {
                        calc3 = Mathf.Atan2((curPos3.y - startPos3.y), curPos3.x - startPos3.x) * 57.2958;

                        if (calc3 < -157.5 || calc3 >= 157.5)
                        {
                            dirArray3[testCount] = 'A';
                            dir3 = 'A';
                        }
                        else if (calc3 < 157.5 && calc3 >= 112.5)
                        {
                            dirArray3[testCount] = 'B';
                            dir3 = 'B';
                        }
                        else if (calc3 < 112.5 && calc3 >= 67.5)
                        {
                            dirArray3[testCount] = 'C';
                            dir3 = 'C';
                        }
                        else if (calc3 < 67.5 && calc3 >= 22.5)
                        {
                            dirArray3[testCount] = 'D';
                            dir3 = 'D';
                        }
                        else if (calc3 < 22.5 && calc3 >= -22.5)
                        {
                            dirArray3[testCount] = 'E';
                            dir3 = 'E';
                        }
                        else if (calc3 < -22.5 && calc3 >= -67.5)
                        {
                            dirArray3[testCount] = 'F';
                            dir3 = 'F';
                        }
                        else if (calc3 < -67.5 && calc3 >= -112.5)
                        {
                            dirArray3[testCount] = 'G';
                            dir3 = 'G';
                        }
                        else if (calc3 < -112.5 && calc3 > -157.5)
                        {
                            dirArray3[testCount] = 'H';
                            dir3 = 'H';
                        }
                    }
                    if (touchCount > 3)
                    {
                        calc4 = Mathf.Atan2((curPos4.y - startPos4.y), curPos4.x - startPos4.x) * 57.2958;

                        if (calc4 < -157.5 || calc4 >= 157.5)
                        {
                            dirArray4[testCount] = 'A';
                            dir4 = 'A';
                        }
                        else if (calc4 < 157.5 && calc4 >= 112.5)
                        {
                            dirArray4[testCount] = 'B';
                            dir4 = 'B';
                        }
                        else if (calc4 < 112.5 && calc4 >= 67.5)
                        {
                            dirArray4[testCount] = 'C';
                            dir4 = 'C';
                        }
                        else if (calc4 < 67.5 && calc4 >= 22.5)
                        {
                            dirArray4[testCount] = 'D';
                            dir4 = 'D';
                        }
                        else if (calc4 < 22.5 && calc4 >= -22.5)
                        {
                            dirArray4[testCount] = 'E';
                            dir4 = 'E';
                        }
                        else if (calc4 < -22.5 && calc4 >= -67.5)
                        {
                            dirArray4[testCount] = 'F';
                            dir4 = 'F';
                        }
                        else if (calc4 < -67.5 && calc4 >= -112.5)
                        {
                            dirArray4[testCount] = 'G';
                            dir4 = 'G';
                        }
                        else if (calc4 < -112.5 && calc4 > -157.5)
                        {
                            dirArray4[testCount] = 'H';
                            dir4 = 'H';
                        }
                    }
                #endregion

                #region Finding gesture
                if (touchCount == 1)
                {
                    if (finGesture == "SX" || finGesture == "SL" || finGesture == "SR")
                    {
                        if (dirArray1[0] == 'A' && dirArray1[1] == 'A' && dirArray1[2] == 'A') finGesture = "SL";
                        else if (dirArray1[0] == 'E' && dirArray1[1] == 'E' && dirArray1[2] == 'E') finGesture = "SR";
                    }

                    if (finGesture == "SX" || finGesture == "SU" || finGesture == "SD")
                    { 
                        if (dirArray1[0] == 'C' && dirArray1[1] == 'C' && dirArray1[2] == 'C') finGesture = "SU";
                        else if (dirArray1[0] == 'G' && dirArray1[1] == 'G' && dirArray1[2] == 'G') finGesture = "SD";
                    }
                    if (finGesture == "SX" || finGesture == "RB" || finGesture == "RF" || finGesture == "SU" || finGesture == "SD" || finGesture == "SL" || finGesture == "SR")
                    {
                        if (dirArray1[0] == 'A' && dirArray1[1] == 'B' && dirArray1[2] == 'C') finGesture = "RF";
                        else if (dirArray1[0] == 'B' && dirArray1[1] == 'C' && dirArray1[2] == 'D') finGesture = "RF";
                        else if (dirArray1[0] == 'C' && dirArray1[1] == 'D' && dirArray1[2] == 'E') finGesture = "RF";
                        else if (dirArray1[0] == 'D' && dirArray1[1] == 'E' && dirArray1[2] == 'F') finGesture = "RF";
                        else if (dirArray1[0] == 'E' && dirArray1[1] == 'F' && dirArray1[2] == 'G') finGesture = "RF";
                        else if (dirArray1[0] == 'F' && dirArray1[1] == 'G' && dirArray1[2] == 'H') finGesture = "RF";
                        else if (dirArray1[0] == 'G' && dirArray1[1] == 'H' && dirArray1[2] == 'A') finGesture = "RF";
                        else if (dirArray1[0] == 'H' && dirArray1[1] == 'A' && dirArray1[2] == 'B') finGesture = "RF";

                        else if (dirArray1[0] == 'A' && dirArray1[1] == 'H' && dirArray1[2] == 'G') finGesture = "RB";
                        else if (dirArray1[0] == 'H' && dirArray1[1] == 'G' && dirArray1[2] == 'F') finGesture = "RB";
                        else if (dirArray1[0] == 'G' && dirArray1[1] == 'F' && dirArray1[2] == 'E') finGesture = "RB";
                        else if (dirArray1[0] == 'F' && dirArray1[1] == 'E' && dirArray1[2] == 'D') finGesture = "RB";
                        else if (dirArray1[0] == 'E' && dirArray1[1] == 'D' && dirArray1[2] == 'C') finGesture = "RB";
                        else if (dirArray1[0] == 'D' && dirArray1[1] == 'C' && dirArray1[2] == 'B') finGesture = "RB";
                        else if (dirArray1[0] == 'C' && dirArray1[1] == 'B' && dirArray1[2] == 'A') finGesture = "RB";
                        else if (dirArray1[0] == 'B' && dirArray1[1] == 'A' && dirArray1[2] == 'H') finGesture = "RB";
                    }
                    
                }
                if (touchCount == 2)
                {
                    if (finGesture == "SX" || finGesture == "CI" || finGesture == "CO")
                    { 
                        if (dirArray1[0] == 'A' && dirArray1[1] == 'A' && dirArray1[2] == 'A' &&
                            dirArray2[0] == 'A' && dirArray2[1] == 'A' && dirArray2[2] == 'A') finGesture = "CO";
                        else if (dirArray1[0] == 'E' && dirArray1[1] == 'E' && dirArray1[2] == 'E' &&
                                 dirArray2[0] == 'E' && dirArray2[1] == 'E' && dirArray2[2] == 'E') finGesture = "CI";
                    }
                    if (finGesture == "SX" || finGesture == "AU" || finGesture == "AD")
                    {
                        if (dirArray1[0] == 'C' && dirArray1[1] == 'C' && dirArray1[2] == 'C' &&
                                 dirArray2[0] == 'C' && dirArray2[1] == 'C' && dirArray2[2] == 'C') finGesture = "AU";
                        else if (dirArray1[0] == 'G' && dirArray1[1] == 'G' && dirArray1[2] == 'G' &&
                                 dirArray2[0] == 'G' && dirArray2[1] == 'G' && dirArray2[2] == 'G') finGesture = "AD";
                    }
                    if (finGesture == "SX" || finGesture == "UA" || finGesture == "UF" || finGesture == "AU" || finGesture == "AD" || finGesture == "CI" || finGesture == "CO")
                    {
                        if (dirArray1[0] == 'A' && dirArray1[1] == 'B' && dirArray1[2] == 'C' &&
                                 dirArray2[0] == 'A' && dirArray2[1] == 'B' && dirArray2[2] == 'C') finGesture = "UF";
                        else if (dirArray1[0] == 'B' && dirArray1[1] == 'C' && dirArray1[2] == 'D' &&
                                 dirArray2[0] == 'B' && dirArray2[1] == 'C' && dirArray2[2] == 'D') finGesture = "UF";
                        else if (dirArray1[0] == 'C' && dirArray1[1] == 'D' && dirArray1[2] == 'E' &&
                                 dirArray2[0] == 'C' && dirArray2[1] == 'D' && dirArray2[2] == 'E') finGesture = "UF";
                        else if (dirArray1[0] == 'D' && dirArray1[1] == 'E' && dirArray1[2] == 'F' &&
                                 dirArray2[0] == 'D' && dirArray2[1] == 'E' && dirArray2[2] == 'F') finGesture = "UF";
                        else if (dirArray1[0] == 'E' && dirArray1[1] == 'F' && dirArray1[2] == 'G' &&
                                 dirArray2[0] == 'E' && dirArray2[1] == 'F' && dirArray2[2] == 'G') finGesture = "UF";
                        else if (dirArray1[0] == 'F' && dirArray1[1] == 'G' && dirArray1[2] == 'H' &&
                                 dirArray2[0] == 'F' && dirArray2[1] == 'G' && dirArray2[2] == 'H') finGesture = "UF";
                        else if (dirArray1[0] == 'G' && dirArray1[1] == 'H' && dirArray1[2] == 'A' &&
                                 dirArray2[0] == 'G' && dirArray2[1] == 'H' && dirArray2[2] == 'A') finGesture = "UF";
                        else if (dirArray1[0] == 'H' && dirArray1[1] == 'A' && dirArray1[2] == 'B' &&
                                 dirArray2[0] == 'H' && dirArray2[1] == 'A' && dirArray2[2] == 'B') finGesture = "UF";

                        else if (dirArray1[0] == 'A' && dirArray1[1] == 'H' && dirArray1[2] == 'G' &&
                                 dirArray2[0] == 'A' && dirArray2[1] == 'H' && dirArray2[2] == 'G') finGesture = "UA";
                        else if (dirArray1[0] == 'H' && dirArray1[1] == 'G' && dirArray1[2] == 'F' &&
                                 dirArray2[0] == 'H' && dirArray2[1] == 'G' && dirArray2[2] == 'F') finGesture = "UA";
                        else if (dirArray1[0] == 'G' && dirArray1[1] == 'F' && dirArray1[2] == 'E' &&
                                 dirArray2[0] == 'G' && dirArray2[1] == 'F' && dirArray2[2] == 'E') finGesture = "UA";
                        else if (dirArray1[0] == 'F' && dirArray1[1] == 'E' && dirArray1[2] == 'D' &&
                                 dirArray2[0] == 'F' && dirArray2[1] == 'E' && dirArray2[2] == 'D') finGesture = "UA";
                        else if (dirArray1[0] == 'E' && dirArray1[1] == 'D' && dirArray1[2] == 'C' &&
                                 dirArray2[0] == 'E' && dirArray2[1] == 'D' && dirArray2[2] == 'C') finGesture = "UA";
                        else if (dirArray1[0] == 'D' && dirArray1[1] == 'C' && dirArray1[2] == 'B' &&
                                 dirArray2[0] == 'D' && dirArray2[1] == 'C' && dirArray2[2] == 'B') finGesture = "UA";
                        else if (dirArray1[0] == 'C' && dirArray1[1] == 'B' && dirArray1[2] == 'A' &&
                                 dirArray2[0] == 'C' && dirArray2[1] == 'B' && dirArray2[2] == 'A') finGesture = "UA";
                        else if (dirArray1[0] == 'B' && dirArray1[1] == 'A' && dirArray1[2] == 'H' &&
                                 dirArray2[0] == 'B' && dirArray2[1] == 'A' && dirArray2[2] == 'H') finGesture = "UA";
                    }
                    }
                if (touchCount == 3)
                {
                    if (dirArray1[0] == 'A' && dirArray1[1] == 'A' && dirArray1[2] == 'A' &&
                        dirArray2[0] == 'A' && dirArray2[1] == 'A' && dirArray2[2] == 'A' &&
                        dirArray3[0] == 'A' && dirArray3[1] == 'A' && dirArray3[2] == 'A') finGesture = "LF";
                    else if (dirArray1[0] == 'E' && dirArray1[1] == 'E' && dirArray1[2] == 'E' &&
                                dirArray2[0] == 'E' && dirArray2[1] == 'E' && dirArray2[2] == 'E' &&
                                dirArray3[0] == 'E' && dirArray3[1] == 'E' && dirArray3[2] == 'E') finGesture = "LV";

                }
                if (touchCount == 4)
                {
                    if (dirArray1[0] == 'C' && dirArray1[1] == 'C' && dirArray1[2] == 'C' &&
                        dirArray2[0] == 'C' && dirArray2[1] == 'C' && dirArray2[2] == 'C' &&
                        dirArray3[0] == 'C' && dirArray3[1] == 'C' && dirArray3[2] == 'C' &&
                        dirArray4[0] == 'C' && dirArray4[1] == 'C' && dirArray4[2] == 'C') finGesture = "HU";
                    else if (dirArray1[0] == 'G' && dirArray1[1] == 'G' && dirArray1[2] == 'G' &&
                                dirArray2[0] == 'G' && dirArray2[1] == 'G' && dirArray2[2] == 'G' &&
                                dirArray3[0] == 'G' && dirArray3[1] == 'G' && dirArray3[2] == 'G' &&
                                dirArray4[0] == 'G' && dirArray4[1] == 'G' && dirArray4[2] == 'G') finGesture = "HD";

                }
                    #endregion

                    Debug.Log(finGesture);

                    BtConnector.sendString(finGesture);

                    if (gestureList[0] != finGesture)
                    {
                        gestureList[3] = gestureList[2];
                        gestureList[2] = gestureList[1];
                        gestureList[1] = gestureList[0];
                        gestureList[0] = finGesture;
                    }

                    startPos1 = curPos1;
                    startPos2 = curPos2;
                    startPos3 = curPos3;
                    startPos4 = curPos4;
                }
            
        }

        void OnGUI()
        {
            GUI.Label(new Rect(0, Screen.height * 0.3f, Screen.width, Screen.height * 0.1f), "from PlugIn : " + BtConnector.readControlData());
            GUI.Label(new Rect(0, Screen.height * 0.4f, Screen.width, Screen.height * 0.1f), "Gesture     : " + finGesture);
            GUI.Label(new Rect(0, Screen.height * 0.5f, Screen.width, Screen.height * 0.1f), "Debug       : " + curPos1 + " dir:" + dir1); //debug
            GUI.Label(new Rect(0, Screen.height * 0.55f, Screen.width, Screen.height * 0.1f),"            : " + curPos2 + " dir:" + dir2); //debug
            GUI.Label(new Rect(0, Screen.height * 0.6f, Screen.width, Screen.height * 0.1f), "            : " + curPos3 + " dir:" + dir3); //debug
            GUI.Label(new Rect(0, Screen.height * 0.65f, Screen.width, Screen.height * 0.1f),"            : " + curPos4 + " dir:" + dir4); //debug

            GUI.Label(new Rect(Screen.width * 0.95f, Screen.height * 0.40f, Screen.width * 0.5f, Screen.height * 0.1f), gestureList[0]);
            GUI.Label(new Rect(Screen.width * 0.95f, Screen.height * 0.45f, Screen.width * 0.5f, Screen.height * 0.1f), gestureList[1]);
            GUI.Label(new Rect(Screen.width * 0.95f, Screen.height * 0.50f, Screen.width * 0.5f, Screen.height * 0.1f), gestureList[2]);
            GUI.Label(new Rect(Screen.width * 0.95f, Screen.height * 0.55f, Screen.width * 0.5f, Screen.height * 0.1f), gestureList[3]);

        }
        #endregion

        #region Event handlers Modified for Total Touch
        private void touchesBeganHandler(object sender, TouchEventArgs e)
        {

            if (touchProxy == null) return;

            var count = e.Touches.Count;

            touchCount = e.Touches.Count;

            for (var i = 0; i < count; i++)
            {
                var touch = e.Touches[i];
                var proxy = pool.Get();



                proxy.Size = getTouchSize();
                proxy.ShowTouchId = showTouchId;
                proxy.ShowTags = showTags;
                proxy.Init(rect, touch);
                proxies.Add(touch.Id, proxy);
            }

            if (touchCount == 1)
            {
                startPos1 = e.Touches[0].Position;
            }
            if (touchCount == 2)
            {
                startPos1 = e.Touches[0].Position;
                startPos2 = e.Touches[1].Position;
            }
            if (touchCount == 3)
            {
                startPos1 = e.Touches[0].Position;
                startPos2 = e.Touches[1].Position;
                startPos3 = e.Touches[2].Position;
            }
            if (touchCount == 4)
            {
                startPos1 = e.Touches[0].Position;
                startPos2 = e.Touches[1].Position;
                startPos3 = e.Touches[2].Position;
                startPos4 = e.Touches[3].Position;
            }



        }

        private void touchesMovedHandler(object sender, TouchEventArgs e)
        {
            var count = e.Touches.Count;

            touchCount = e.Touches.Count;

            for (var i = 0; i < count; i++)
            {
                var touch = e.Touches[i];

                TouchProxyBase proxy;
                if (!proxies.TryGetValue(touch.Id, out proxy)) return;
                proxy.UpdateTouch(touch);
            }

            if (touchCount == 1)
            {
                curPos1 = e.Touches[0].Position;
            }
            if (touchCount == 2)
            {
                curPos1 = e.Touches[0].Position;
                curPos2 = e.Touches[1].Position;
            }
            if (touchCount == 3)
            {
                curPos1 = e.Touches[0].Position;
                curPos2 = e.Touches[1].Position;
                curPos3 = e.Touches[2].Position;
            }
            if (touchCount == 4)
            {
                curPos1 = e.Touches[0].Position;
                curPos2 = e.Touches[1].Position;
                curPos3 = e.Touches[2].Position;
                curPos4 = e.Touches[3].Position;
            }


        }

        private void touchesEndedHandler(object sender, TouchEventArgs e)
        {
            var count = e.Touches.Count;



            for (var i = 0; i < count; i++)
            {
                var touch = e.Touches[i];
                TouchProxyBase proxy;
                if (!proxies.TryGetValue(touch.Id, out proxy)) return;
                proxies.Remove(touch.Id);
                pool.Release(proxy);
            }

            touchCount = 0;
            finGesture = "SX";
            BtConnector.sendString(finGesture);
            Debug.Log("SX");
            for (int i = 0; i < 3; i++)
            {
                dirArray1[i] = 'X';
                dirArray2[i] = 'X';
                dirArray3[i] = 'X';
                dirArray4[i] = 'X';
            }
        }
        #endregion
        #endregion 
    }
}