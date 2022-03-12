using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

// using System.Random;

// using readingData;

using MethodExtend;


public class C0CSWordGame_V_0_0_3: MonoBehaviour
{


    static string pathResources = "D:\\INTERNET\\Wiki-Andresito-07-WORK\\WordGame_V_0_0_1\\Assets\\Resources\\";

    private int HEIGHT = 1000;
    private int WIDTH  = 2000;
    private float time;


    public float updateInterval = 0.5F;
    private double lastInterval;
    private int frames;
    private float fps;
    
    private float changeInterval;

    public const int CHARACTERWIDTH = 34;

    Renderer render;
    Material mat;

    public ComputeShader computacion;

    ComputeBuffer bufferImageColor;

    ComputeBuffer bufferRectangleHorizontalPosition;
    ComputeBuffer bufferRectangleHorizontalColor;

    ComputeBuffer bufferRectangleVerticalPosition;
    ComputeBuffer bufferRectangleVerticalColor;

    ComputeBuffer bufferTextureCharacterPositionX;
    ComputeBuffer bufferTextureCharacterPositionY;
    ComputeBuffer bufferTextureCharacterColor;

    ComputeBuffer bufferTextureNumberPositionX;
    ComputeBuffer bufferTextureNumberPositionY;
    ComputeBuffer bufferTextureNumberColor;


    static int _kernelforPrintRectangleHorizontal;
    static int _kernelforPrintRectangleVertical;

    static int _kernelforLoadTextureCharacterPosition;
    static int _kernelforPrintTilesCharacter;
    static int _kernelforPrintTilesCharacterIndividual;

    static int _kernelforLoadTextureNumberPosition;
    static int _kernelforPrintTilesNumberIndividual;



    float[] arrayLoadData = new float[40000];

    static float[] dataToLoadColor = new float[5456];

    static float[] dataToLoadPosition = new float[2728]; 


    // static float[] componenteCodigo = new float[20000];
    // static float[] componenteDefinition = new float[200000];
    // static float[] detailElementosMind = new float[200000];
    // static float[] analisisMind = new float[200000];


    bool activo = false;
    bool activo2 = false;


    static List<string> wordLanguage0 = new List<string>();
    static List<string> wordLanguage1 = new List<string>();

    static string[] listOfWordToPaint = new string[22];

    List<string> listOfWordInput = new List<string>();

    string StringTextField = "";

    int countPositionCharacter = 0;

    bool setFlagFind = false;
    

    int _YaxisTextField = 955;
    int _XaxisTextField = 1967;


    void OnGUI()
    {
        GUILayout.Label("" + fps.ToString("f2"));
    }

    
    // Start is called before the first frame update

    void Start()
    {


        Debug.Log(readingData.lectura.definition);
        Debug.Log(readingData.lectura.elemento);

        Debug.Log(readingData.lectura.compositionCodigo());



        render = GetComponent<Renderer>();
        mat = render.material;

        bufferImageColor = new ComputeBuffer(HEIGHT * WIDTH, 16);

        mat.SetBuffer("bufferImageColor", bufferImageColor);
    
        computacion.SetInt("_WIDTH", WIDTH);

        mat.SetInt("_WIDTH", WIDTH);
        mat.SetInt("_HEIGHT", HEIGHT);


        _kernelforPrintRectangleHorizontal = computacion.FindKernel("forPrintRectangleHorizontal");
        _kernelforPrintRectangleVertical = computacion.FindKernel("forPrintRectangleVertical");

        _kernelforLoadTextureCharacterPosition = computacion.FindKernel("forLoadTextureCharacterPosition");
        _kernelforPrintTilesCharacter = computacion.FindKernel("forPrintTilesCharacter");
        _kernelforPrintTilesCharacterIndividual = computacion.FindKernel("forPrintTilesCharacterIndividual");


        _kernelforLoadTextureNumberPosition = computacion.FindKernel("forLoadTextureNumberPosition");
        _kernelforPrintTilesNumberIndividual = computacion.FindKernel("forPrintTilesNumberIndividual");


        // computacion.SetBuffer(_kernel)

        bufferRectangleHorizontalPosition = new ComputeBuffer(2728, 4);
        bufferRectangleHorizontalColor= new ComputeBuffer(5456, 4);

        bufferRectangleVerticalPosition = new ComputeBuffer(2728, 4);
        bufferRectangleVerticalColor = new ComputeBuffer(5456, 4);

        bufferTextureCharacterPositionX = new ComputeBuffer(38192, 4);
        bufferTextureCharacterPositionY = new ComputeBuffer(38192, 4);
        bufferTextureCharacterColor = new ComputeBuffer(152768, 4); 

        bufferTextureNumberPositionX = new ComputeBuffer(38192, 4);
        bufferTextureNumberPositionY = new ComputeBuffer(38192, 4);
        bufferTextureNumberColor = new ComputeBuffer(152768, 4); 



        computacion.SetBuffer(_kernelforPrintRectangleHorizontal, "bufferImageColor", bufferImageColor);
        computacion.SetBuffer(_kernelforPrintRectangleHorizontal, "bufferRectangleHorizontalPosition", bufferRectangleHorizontalPosition);
        computacion.SetBuffer(_kernelforPrintRectangleHorizontal, "bufferRectangleHorizontalColor", bufferRectangleHorizontalColor);

        computacion.SetBuffer(_kernelforPrintRectangleVertical, "bufferImageColor", bufferImageColor);
        computacion.SetBuffer(_kernelforPrintRectangleVertical, "bufferRectangleVerticalPosition", bufferRectangleVerticalPosition);
        computacion.SetBuffer(_kernelforPrintRectangleVertical, "bufferRectangleVerticalColor", bufferRectangleVerticalColor);

        computacion.SetBuffer(_kernelforLoadTextureCharacterPosition, "bufferTextureCharacterPositionX", bufferTextureCharacterPositionX);
        computacion.SetBuffer(_kernelforLoadTextureCharacterPosition, "bufferTextureCharacterPositionY", bufferTextureCharacterPositionY);

        computacion.SetBuffer(_kernelforPrintTilesCharacter,"bufferImageColor", bufferImageColor);
        computacion.SetBuffer(_kernelforPrintTilesCharacter,"bufferTextureCharacterPositionX", bufferTextureCharacterPositionX);
        computacion.SetBuffer(_kernelforPrintTilesCharacter,"bufferTextureCharacterPositionY", bufferTextureCharacterPositionY);
        computacion.SetBuffer(_kernelforPrintTilesCharacter,"bufferTextureCharacterColor", bufferTextureCharacterColor);

        computacion.SetBuffer(_kernelforPrintTilesCharacterIndividual,"bufferImageColor", bufferImageColor);
        computacion.SetBuffer(_kernelforPrintTilesCharacterIndividual,"bufferTextureCharacterPositionX", bufferTextureCharacterPositionX);
        computacion.SetBuffer(_kernelforPrintTilesCharacterIndividual,"bufferTextureCharacterPositionY", bufferTextureCharacterPositionY);
        computacion.SetBuffer(_kernelforPrintTilesCharacterIndividual,"bufferTextureCharacterColor", bufferTextureCharacterColor);

        computacion.SetBuffer(_kernelforLoadTextureNumberPosition, "bufferTextureNumberPositionX", bufferTextureNumberPositionX);
        computacion.SetBuffer(_kernelforLoadTextureNumberPosition, "bufferTextureNumberPositionY", bufferTextureNumberPositionY);

        computacion.SetBuffer(_kernelforPrintTilesNumberIndividual,"bufferImageColor", bufferImageColor);
        computacion.SetBuffer(_kernelforPrintTilesNumberIndividual,"bufferTextureNumberPositionX", bufferTextureNumberPositionX);
        computacion.SetBuffer(_kernelforPrintTilesNumberIndividual,"bufferTextureNumberPositionY", bufferTextureNumberPositionY);
        computacion.SetBuffer(_kernelforPrintTilesNumberIndividual,"bufferTextureNumberColor", bufferTextureNumberColor);



    }


/////////////////////////////////////////////////////////////////////////////////////////////////////////
//  UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE  //
/////////////////////////////////////////////////////////////////////////////////////////////////////////




    // Update is called once per frame
    void Update()
    {   


        ++frames;
        
        float timeNow = Time.realtimeSinceStartup;

        // Debug.Log(timeNow);

        if (timeNow > lastInterval + updateInterval)
        {

            fps = (float)(frames / (timeNow - lastInterval));
            frames = 0;
            lastInterval = timeNow;

        }  


        if(Input.GetKeyUp(KeyCode.Return))
        {

            activo = !activo;        

            paintBound();
            paintCharacterFromTexture();
            paintCharacterFromTextureIndividual(500,100,3,5);

            for(int i = 0; i < 200; ++i)
            {
                wordLanguage0.Add("");
            }
    
            for(int i = 0; i < 200; ++i)
            {
                wordLanguage1.Add("");
            }


            string fileName0 = "English-Italian-G1.txt";
            string fileName1 = "Italian-English-G1.txt";
    
            loadWordList(fileName0, fileName1);
            WordsList();

            for(int i = 0; i < 11; ++i)
            {
                Debug.Log(listOfWordToPaint[i] + "    " + listOfWordToPaint[i + 11]);
            }


            Debug.Log(listOfWordToPaint[0]);
            
            

        }



        if(Input.GetKeyUp(KeyCode.F1))
        {


            readDataPosition("RectangleHorizontalPosition.txt");
            readDataColor("RectangleHorizontalColor.txt");

            bufferRectangleHorizontalPosition.SetData(dataToLoadPosition);
            bufferRectangleHorizontalColor.SetData(dataToLoadColor);

            readDataPosition("RectangleVerticalPosition.txt");
            readDataColor("RectangleVerticalColor.txt");

            bufferRectangleVerticalPosition.SetData(dataToLoadPosition);
            bufferRectangleVerticalColor.SetData(dataToLoadColor);

            loadBufferTextureCharacter();

            loadBufferTextureNumber();


        }


        if(activo == true && StringTextField.Length <= 30)
        {

            getKeyBoardInput();
                                    

        }
        

        if(activo == true && Input.GetKeyUp(KeyCode.Backspace) && StringTextField.Length > 0)
        {

            string empty = "";
            
            for(int i = 0; i < StringTextField.Length - 1 ; ++i)
            {
                empty += StringTextField[i];
            }

            StringTextField = empty;

            _XaxisTextField += CHARACTERWIDTH; 


            int _PositionX = _XaxisTextField;
            int _PositionY = _YaxisTextField;

            paintCharacterFromTextureIndividual(_PositionX, _PositionY,2,6);
            

        }




        if(Input.GetKeyUp(KeyCode.F3) && StringTextField.Length > 0)
        {

            paintWordPosition(965,20,StringTextField);

            System.Random random = new System.Random();

            int positionRecX = random.Next(1, 2000);
            int positionRecY = random.Next(1, 1000);


            forPrintRectangleHorizontal(positionRecX, positionRecY);
            forPrintRectangleVertical(positionRecX + 34, positionRecY);

        }


        
        
        if(Input.GetKeyUp(KeyCode.F10))
        {
            
            paintWordPosition(1000, 100, "HELLO");
        }
        
       



        if(Input.GetKeyUp(KeyCode.F2) && StringTextField.Length > 0)
        {


            int propiedadNumerica = 75;
            
            for(int i = 0; i < 11; ++i)
            {
                
                {
                    // paintWordPosition(965, 20 + propiedadNumerica * i , stringToWriteRigth[i]);
                    
                    paintWordPosition(965, 20 + propiedadNumerica * i , StringTextField );

                }

            }


            
            for(int i = 0; i < 11; ++i)
            {
                
                {
                    // paintWordPosition(965, 20 + propiedadNumerica * i , stringToWriteLeft[i]);

                    paintWordPosition(1865, 20 + propiedadNumerica * i , StringTextField);

                }

            }

        }

        Debug.Log(StringTextField);

    }

////////////////////////////////////////////////////////////////////////////////////////////////////////
//  UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE  //
/////////////////////////////////////////////////////////////////////////////////////////////////////////




/////////////////////////////////////////////////////////////////////////////////////////////////////////
//   DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY   //
/////////////////////////////////////////////////////////////////////////////////////////////////////////[


    void OnDestroy()
    {   


        bufferImageColor.Dispose();
        bufferImageColor.Release();
        bufferImageColor = null;

        bufferRectangleHorizontalPosition.Dispose();
        bufferRectangleHorizontalPosition.Release();
        bufferRectangleHorizontalPosition = null;

        bufferRectangleHorizontalColor.Dispose();
        bufferRectangleHorizontalColor.Release();
        bufferRectangleHorizontalColor = null;

        bufferRectangleVerticalPosition.Dispose();
        bufferRectangleVerticalPosition.Release();
        bufferRectangleVerticalPosition = null;

        bufferRectangleVerticalColor.Dispose();
        bufferRectangleVerticalColor.Release();
        bufferRectangleVerticalColor = null;

        bufferTextureCharacterPositionX.Dispose();
        bufferTextureCharacterPositionX.Release();
        bufferTextureCharacterPositionX = null;

        bufferTextureCharacterPositionY.Dispose();
        bufferTextureCharacterPositionY.Release();
        bufferTextureCharacterPositionY = null;

        bufferTextureCharacterColor.Dispose();
        bufferTextureCharacterColor.Release();
        bufferTextureCharacterColor = null;


    }


/////////////////////////////////////////////////////////////////////////////////////////////////////////
//   DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY   //
/////////////////////////////////////////////////////////////////////////////////////////////////////////
//   sense how to define notions and concepts learn how to define mathematics                          //
/////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static void loadWordList(string fileName0, string fileName1)
    {

        //IMAGEDATA
        //letterQPosition   


        string fileNameLanguage0 = fileName0;
        string fileNameLanguage1 = fileName1; 

        string path0 = pathResources + fileNameLanguage0;
        string path1 = pathResources + fileNameLanguage1;

        //dataToLoadColor;


        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path0);

        string lecturaDatos0 = reader.ReadToEnd();

        reader.Close();

        reader = new StreamReader(path1);

        string lecturaDatos1 = reader.ReadToEnd();


        int count = 0;

        for(int i = 0; i < lecturaDatos0.Length; ++i)
        {


            if( lecturaDatos0[i].ToString() != "\n")
            {
        
                wordLanguage0[count] += lecturaDatos0[i].ToString();

            }

            else
            {
 
                count ++;
 
            }

            if(count == 200)
            {
                break;
            }


        }

        count = 0;

        for(int i = 0; i < lecturaDatos1.Length; ++i)
        {


            if( lecturaDatos1[i].ToString() != "\n")
            {
        
                wordLanguage1[count] += lecturaDatos1[i].ToString();

            }

            else
            {
 
                count ++;
 
            }

            if(count == 200)
            {
                break;
            }


        }



    }

    
    public void WordsList()
    {

        System.Random elemento = new System.Random();

        for(int i = 0; i < 11; ++i)
        {


            string word0;

            while(true)
            {
                word0 = wordLanguage0[elemento.Next(0, 199)] ;

                if(word0 != "")
                {
                    break;
                }
            }


            string word1;

            while(true)
            {

                word1 = wordLanguage1[elemento.Next(0, 199)] ;

                if(word1 != "")
                {
                    break;
                }

            }


            listOfWordToPaint[i + 11] = word1;
            listOfWordToPaint[i] = word0;
        }
 
    }




    public void forPrintRectangleHorizontal(int Xaxis, int Yaxis)
    {

        int _PositionX = Xaxis;
        int _PositionY = Yaxis;

        int _ArrayPositionPrintRectangleHorizontal = 0;

        computacion.SetInt("_PositionXRectangleHorizontal", _PositionX);
        computacion.SetInt("_PositionYRectangleHorizontal", _PositionY);

        for(int i = 0; i < 2 ; ++i)
        {

            computacion.SetInt("_ArrayPositionPrintRectangleHorizontal", _ArrayPositionPrintRectangleHorizontal);
            computacion.Dispatch(_kernelforPrintRectangleHorizontal,1,1,1);
            _ArrayPositionPrintRectangleHorizontal += 682;

        }

    }




    public void forPrintRectangleVertical(int Xaxis, int Yaxis)
    {

        int _PositionX = Xaxis;
        int _PositionY = Yaxis;

        int _ArrayPositionPrintRectangleVertical = 0;

        computacion.SetInt("_PositionXRectangleVertical", _PositionX);
        computacion.SetInt("_PositionYRectangleVertical", _PositionY);

        for(int i = 0; i < 2 ; ++i)
        {

            computacion.SetInt("_ArrayPositionPrintRectangleVertical", _ArrayPositionPrintRectangleVertical);
            computacion.Dispatch(_kernelforPrintRectangleVertical,1,1,1);
            _ArrayPositionPrintRectangleVertical += 682;

        }


    }




    public static void readDataColor(string fileNameColor)
    {
        
              // IMAGEDATA
        // letterQPosition

        string fileName = fileNameColor;

        string path = pathResources + fileName;

        //dataToLoadColor;


        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);

        string lecturaDatos = reader.ReadToEnd();

//        float[] dataToSet = new float[5456];
 
        string lecturaString = "";

        int countDataToSet = 0;



        for(int i = 0 ; i < 1364; ++i)
        {

            for(int j = 0 ; j < 53; ++j )
            {

                if(j == 0) 
                {
                }
                else if(j % 13 == 0)
                {

                    dataToLoadColor[countDataToSet] = float.Parse(lecturaString);

                    lecturaString = "";
                    countDataToSet ++;

                }
                else
                {
                    lecturaString += lecturaDatos[i * 54 + j];
                }

            }

        }


        reader.Close();

    }




        public static void readDataPosition(string fileNamePosition)
    {
        // letterQPosition
        // IMAGEPOSITION

        string fileName = fileNamePosition;

        string path = pathResources + fileName;

        // dataToLoadPosition;



        //Read the text from directly from the test.txt file

        StreamReader reader = new StreamReader(path);

        string lecturaDatos = reader.ReadToEnd();

        Debug.Log(lecturaDatos.Length);


        // float[] dataToSet = new float[2728];


        string lecturaString = "";

        int countDataToSet = 0;

        int count = 0;


        for(int i = 0 ; i < 24552; ++i)
        {

            if( count == 8)
            {}

            else if( count == 9)
            {
                dataToLoadPosition[countDataToSet] = float.Parse(lecturaString);
                // print(lecturaString);
                count = 0;
                countDataToSet++;

                lecturaString = "";
            }

            else
            {
                lecturaString += lecturaDatos[i];
            }

            count ++;


        }


        reader.Close();


    }
    



    public void loadBufferTextureCharacter()
    {

        float[] arrayTextureCharacterColor = new float[152768];
        //positions[1692800];
        string fileTextureCharacter = "TextureOfCharactersColor.txt";

        readDataColorTextureCharacters(fileTextureCharacter, ref arrayTextureCharacterColor);

        bufferTextureCharacterColor.SetData(arrayTextureCharacterColor);

        computacion.Dispatch(_kernelforLoadTextureCharacterPosition, 308, 1, 1);

    }




    public void readDataColorTextureCharacters(string fileNameColor, ref float[] loadTextureCharacter)
    {

        //IMAGEDATA
        //letterQPosition

        string fileName = fileNameColor;

        string path = pathResources + fileName;

        //dataToLoadColor;


        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);

        string lecturaDatos = reader.ReadToEnd();

        //float[] dataToSet = new float[5456];
 
        string lecturaString = "";

        int countDataToSet = 0;



        for(int i = 0 ; i < 38192; ++i)
        {

            for(int j = 0 ; j < 53; ++j )
            {

                if(j == 0) 
                {
                }
                else if(j % 13 == 0)
                {

                    loadTextureCharacter[countDataToSet] = float.Parse(lecturaString);

                    lecturaString = "";
                    countDataToSet ++;

                }
                else
                {
                    lecturaString += lecturaDatos[i * 54 + j];
                }

            }

        }


        reader.Close();

    }




    public void paintCharacterFromTexture()
    {


        computacion.Dispatch(_kernelforPrintTilesCharacter, 308, 1,1);


    }




    public void paintCharacterFromTextureIndividual(int Xaxis, int Yaxis, int Xtile, int Ytile)
    {

        int _PositionScreenX = Xaxis;        
        int _PositionScreenY = Yaxis;
        
        int _PositionTileX = Xtile;        
        int _PositionTileY = Ytile;                
        

        computacion.SetInt("_PositionScreenX", _PositionScreenX);
        computacion.SetInt("_PositionScreenY", _PositionScreenY);

        computacion.SetInt("_PositionTileX", _PositionTileX);
        computacion.SetInt("_PositionTileY", _PositionTileY);

        computacion.Dispatch(_kernelforPrintTilesCharacterIndividual, 44, 1,1);


    }



    //forLoadTextureNumberPosition
    public void loadBufferTextureNumber()
    {

        float[] arrayTextureNumberColor = new float[152768];
        //positions[1692800];
        string fileTextureNumber = "TextureOfNumbersColor.txt";

        readDataColorTextureCharacters(fileTextureNumber, ref arrayTextureNumberColor);

        bufferTextureNumberColor.SetData(arrayTextureNumberColor);

        computacion.Dispatch(_kernelforLoadTextureNumberPosition, 308, 1, 1);

    }

    public void paintNumberFromTextureIndividual(int Xaxis, int Yaxis, int Xtile, int Ytile)
    {

        int _PositionScreenX = Xaxis;        
        int _PositionScreenY = Yaxis;
        
        int _PositionTileX = Xtile;        
        int _PositionTileY = Ytile;                
        

        computacion.SetInt("_PositionScreenNumberX", _PositionScreenX);
        computacion.SetInt("_PositionScreenNumberY", _PositionScreenY);

        computacion.SetInt("_PositionTileNumberX", _PositionTileX);
        computacion.SetInt("_PositionTileNumberY", _PositionTileY);

        computacion.Dispatch(_kernelforPrintTilesNumberIndividual, 44, 1,1);

    }



    public void loadWordList(string fileNameColor, ref float[] loadTextureCharacter)
    {

        //IMAGEDATA
        //letterQPosition

        string fileName = fileNameColor;

        string path = pathResources + fileName;

        //dataToLoadColor;


        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);

        string lecturaDatos = reader.ReadToEnd();

        //float[] dataToSet = new float[5456];
 
        string lecturaString = "";

        int countDataToSet = 0;



        reader.Close();

    }





    public void paintWordPosition(int Xaxis, int Yaxis, string manageString)
    {

        // System.Random random = new System.Random();
        // int _PositionTranslateX = 965; 
        // int _PositionTranslateY = 20;

        int _PositionTranslateX = Xaxis; 
        int _PositionTranslateY = Yaxis;


        string inputString = manageString;


        for(int i = 0; i < inputString.Length; ++i) 
        {

            string characterToPaint = inputString[i].ToString();

            if(characterToPaint == "A")
            {
                 paintCharacterFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 0, 0);
                 _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "B")
            {
                 paintCharacterFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 1, 0);
                 _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "C")
            {
                 paintCharacterFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 2, 0);
                 _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "D")
            {
                 paintCharacterFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 3, 0);
                 _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "E")
            {
                 paintCharacterFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 0, 1);
                 _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "F")
            {
                 paintCharacterFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 1, 1);
                 _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "G")
            {
                 paintCharacterFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 2, 1);
                 _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "H")
            {
                 paintCharacterFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 3, 1);
                 _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "I")
            {
                 paintCharacterFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 0, 2);
                 _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "J")
            {
                 paintCharacterFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 1, 2);
                 _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "K")
            {
                 paintCharacterFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 2, 2);
                 _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "L")
            {
                 paintCharacterFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 3, 2);
                 _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "M")
            {
                 paintCharacterFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 0, 3);
                 _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "N")
            {
                 paintCharacterFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 1, 3);
                 _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "O")
            {
                 paintCharacterFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 2, 3);
                 _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "P")
            {
                 paintCharacterFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 3, 3);
                 _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "Q")
            {
                 paintCharacterFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 0, 4);
                 _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "R")
            {
                 paintCharacterFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 1, 4);
                 _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "S")
            {
                 paintCharacterFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 2, 4);
                 _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "T")
            {
                 paintCharacterFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 3, 4);
                 _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "U")
            {
                 paintCharacterFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 0, 5);
                 _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "V")
            {
                 paintCharacterFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 1, 5);
                 _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "W")
            {
                 paintCharacterFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 2, 5);
                 _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "X")
            {
                 paintCharacterFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 3, 5);
                 _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "Y")
            {
                 paintCharacterFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 0, 6);
                 _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "Z")
            {
                 paintCharacterFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 1, 6);
                 _PositionTranslateX -= CHARACTERWIDTH;
            }




            

        }


    }    


    public void getKeyBoardInput()
    {

            if(Input.GetKeyUp(KeyCode.A))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintCharacterFromTextureIndividual(_PositionX, _PositionY,0,0);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "A";

            }


            if(Input.GetKeyUp(KeyCode.B))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintCharacterFromTextureIndividual(_PositionX, _PositionY,1,0);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "B";

            }


            if(Input.GetKeyUp(KeyCode.C))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintCharacterFromTextureIndividual(_PositionX, _PositionY,2,0);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "C";

            }


            if(Input.GetKeyUp(KeyCode.D))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintCharacterFromTextureIndividual(_PositionX, _PositionY,3,0);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "D";

            }


            if(Input.GetKeyUp(KeyCode.E))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintCharacterFromTextureIndividual(_PositionX, _PositionY,0,1);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "E";

            }


            if(Input.GetKeyUp(KeyCode.F))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintCharacterFromTextureIndividual(_PositionX, _PositionY,1,1);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "F";

            }


            if(Input.GetKeyUp(KeyCode.G))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintCharacterFromTextureIndividual(_PositionX, _PositionY,2,1);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "G";

            }


            if(Input.GetKeyUp(KeyCode.H))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintCharacterFromTextureIndividual(_PositionX, _PositionY,3,1);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "H";

            }


            if(Input.GetKeyUp(KeyCode.I))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintCharacterFromTextureIndividual(_PositionX, _PositionY,0,2);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "I";

            }


            if(Input.GetKeyUp(KeyCode.J))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintCharacterFromTextureIndividual(_PositionX, _PositionY,1,2);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "J";

            }


            if(Input.GetKeyUp(KeyCode.K))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintCharacterFromTextureIndividual(_PositionX, _PositionY,2,2);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "K";

            }


            if(Input.GetKeyUp(KeyCode.L))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintCharacterFromTextureIndividual(_PositionX, _PositionY,3,2);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "L";

            }



            if(Input.GetKeyUp(KeyCode.M))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintCharacterFromTextureIndividual(_PositionX, _PositionY,0,3);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "M";

            }

            if(Input.GetKeyUp(KeyCode.N))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintCharacterFromTextureIndividual(_PositionX, _PositionY,1,3);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "N";

            }

            if(Input.GetKeyUp(KeyCode.O))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintCharacterFromTextureIndividual(_PositionX, _PositionY,2,3);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "O";

            }

            if(Input.GetKeyUp(KeyCode.P))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintCharacterFromTextureIndividual(_PositionX, _PositionY,3,3);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "P";

            }

            if(Input.GetKeyUp(KeyCode.Q))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintCharacterFromTextureIndividual(_PositionX, _PositionY,0,4);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "Q";

            }

            if(Input.GetKeyUp(KeyCode.R))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintCharacterFromTextureIndividual(_PositionX, _PositionY,1,4);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "R";

            }

            if(Input.GetKeyUp(KeyCode.S))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintCharacterFromTextureIndividual(_PositionX, _PositionY,2,4);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "S";

            }

            if(Input.GetKeyUp(KeyCode.T))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintCharacterFromTextureIndividual(_PositionX, _PositionY,3,4);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "T";

            }


            if(Input.GetKeyUp(KeyCode.U))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintCharacterFromTextureIndividual(_PositionX, _PositionY,0,5);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "U";

            }


            if(Input.GetKeyUp(KeyCode.V))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintCharacterFromTextureIndividual(_PositionX, _PositionY,1,5);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "V";

            }


            if(Input.GetKeyUp(KeyCode.W))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintCharacterFromTextureIndividual(_PositionX, _PositionY,2,5);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "W";

            }


            if(Input.GetKeyUp(KeyCode.X))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintCharacterFromTextureIndividual(_PositionX, _PositionY,3,5);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "X";

            }

            if(Input.GetKeyUp(KeyCode.Y))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintCharacterFromTextureIndividual(_PositionX, _PositionY,0,6);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "Y";

            }



            if(Input.GetKeyUp(KeyCode.Z))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintCharacterFromTextureIndividual(_PositionX, _PositionY,1,6);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "Z";

            }

            if(Input.GetKeyUp(KeyCode.Alpha1))
            {

                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintNumberFromTextureIndividual(_PositionX, _PositionY,1,0);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "1";

            }

    }



    public void paintBound()
    {

        

        ///////

        int positionX = 985;
        int positionY = 0;


        for(int i = 0; i < 18 ; ++i)
        {
    
            forPrintRectangleVertical(positionX, positionY);

            positionY += 44;

        }


        positionX = 985;
        positionY = 781;        


        forPrintRectangleVertical(positionX, positionY);
    
        ///////

        ///////
        
        positionX = 1885;
        positionY = 0;        


        for(int i = 0; i < 18 ; ++i)
        {
    
            forPrintRectangleVertical(positionX, positionY);

            positionY += 44;

        }

        positionX = 1885;
        positionY = 781;        


        forPrintRectangleVertical(positionX, positionY);
    
        ///////


        ///////

        positionX = 85;
        positionY = 0;        


        for(int i = 0; i < 18 ; ++i)
        {
    
            forPrintRectangleVertical(positionX, positionY);

            positionY += 44;

        }


        positionX = 85;
        positionY = 781;        


        forPrintRectangleVertical(positionX, positionY);
    
        ///////


        positionX = 916;
        positionY = 925;        


        for(int i = 0; i < 2 ; ++i)
        {
    
            forPrintRectangleVertical(positionX, positionY);

            positionY += 30;

        }



        positionX = 218;
        positionY = 925;        


        for(int i = 0; i < 2 ; ++i)
        {
    
            forPrintRectangleVertical(positionX, positionY);

            positionY += 30;

        }


        positionX = 454;
        positionY = 925;        


        for(int i = 0; i < 2 ; ++i)
        {
    
            forPrintRectangleVertical(positionX, positionY);

            positionY += 30;

        }

        /////////////////////////////////////////////////
        // VERTICAL LINES
        ////////////////////////////////////////////////


        ///DOWN RIGHT
        positionX = 0;
        positionY = 907;        


        for(int i = 0; i < 9 ; ++i)
        {
    
            forPrintRectangleHorizontal(positionX, positionY);

            positionX += 25;

        }



        positionX = 236;
        positionY = 907;        


        for(int i = 0; i < 9 ; ++i)
        {
    
            forPrintRectangleHorizontal(positionX, positionY);
            positionX += 25;

        }


        /// DOWN LEFT
        positionX = 935;
        positionY = 907;        
        
        for(int i = 0; i < 36; ++i)
        {
        
            forPrintRectangleHorizontal(positionX, positionY);
            positionX += 30;
        
        }


        /// UP RIGHT

        positionX = 104;
        positionY = -20;        


        for(int i = 0; i < 33; ++i)
        {
    
            forPrintRectangleHorizontal(positionX, positionY);
            positionX += 27;

        }



        positionX = 104;
        positionY = 50;        


        for(int i = 0; i < 33; ++i)
        {
    
            forPrintRectangleHorizontal(positionX, positionY);
            positionX += 27;

        }



        positionX = 104;
        positionY = 125;        


        for(int i = 0; i < 33; ++i)
        {
    
            forPrintRectangleHorizontal(positionX, positionY);
            positionX += 27;

        }



        positionX = 104;
        positionY = 200;        


        for(int i = 0; i < 33; ++i)
        {
    
            forPrintRectangleHorizontal(positionX, positionY);
            positionX += 27;

        }



        positionX = 104;
        positionY = 275;        


        for(int i = 0; i < 33; ++i)
        {
    
            forPrintRectangleHorizontal(positionX, positionY);
            positionX += 27;

        }



        positionX = 104;
        positionY = 350;        


        for(int i = 0; i < 33; ++i)
        {
    
            forPrintRectangleHorizontal(positionX, positionY);
            positionX += 27;

        }



        positionX = 104;
        positionY = 425;        


        for(int i = 0; i < 33; ++i)
        {
    
            forPrintRectangleHorizontal(positionX, positionY);
            positionX += 27;

        }



        positionX = 104;
        positionY = 500;        


        for(int i = 0; i < 33; ++i)
        {
    
            forPrintRectangleHorizontal(positionX, positionY);
            positionX += 27;

        }




        positionX = 104;
        positionY = 575;        


        for(int i = 0; i < 33; ++i)
        {
    
            forPrintRectangleHorizontal(positionX, positionY);
            positionX += 27;

        }




        positionX = 104;
        positionY = 650;        


        for(int i = 0; i < 33; ++i)
        {
    
            forPrintRectangleHorizontal(positionX, positionY);
            positionX += 27;

        }




        positionX = 104;
        positionY = 725;        


        for(int i = 0; i < 33; ++i)
        {
    
            forPrintRectangleHorizontal(positionX, positionY);
            positionX += 27;

        }




        positionX = 104;
        positionY = 800;        


        for(int i = 0; i < 33; ++i)
        {
    
            forPrintRectangleHorizontal(positionX, positionY);
            positionX += 27;

        }





        /// UP LEFT

        positionX = 1004;
        positionY = -20;        


        for(int i = 0; i < 33; ++i)
        {
    
            forPrintRectangleHorizontal(positionX, positionY);
            positionX += 27;

        }



        positionX = 1004;
        positionY = 50;        


        for(int i = 0; i < 33; ++i)
        {
    
            forPrintRectangleHorizontal(positionX, positionY);
            positionX += 27;

        }



        positionX = 1004;
        positionY = 125;        


        for(int i = 0; i < 33; ++i)
        {
    
            forPrintRectangleHorizontal(positionX, positionY);
            positionX += 27;

        }



        positionX = 1004;
        positionY = 200;        


        for(int i = 0; i < 33; ++i)
        {
    
            forPrintRectangleHorizontal(positionX, positionY);
            positionX += 27;

        }



        positionX = 1004;
        positionY = 275;        


        for(int i = 0; i < 33; ++i)
        {
    
            forPrintRectangleHorizontal(positionX, positionY);
            positionX += 27;

        }



        positionX = 1004;
        positionY = 350;        


        for(int i = 0; i < 33; ++i)
        {
    
            forPrintRectangleHorizontal(positionX, positionY);
            positionX += 27;

        }



        positionX = 1004;
        positionY = 425;        


        for(int i = 0; i < 33; ++i)
        {
    
            forPrintRectangleHorizontal(positionX, positionY);
            positionX += 27;

        }



        positionX = 1004;
        positionY = 500;        


        for(int i = 0; i < 33; ++i)
        {
    
            forPrintRectangleHorizontal(positionX, positionY);
            positionX += 27;

        }




        positionX = 1004;
        positionY = 575;        


        for(int i = 0; i < 33; ++i)
        {
    
            forPrintRectangleHorizontal(positionX, positionY);
            positionX += 27;

        }




        positionX = 1004;
        positionY = 650;        


        for(int i = 0; i < 33; ++i)
        {
    
            forPrintRectangleHorizontal(positionX, positionY);
            positionX += 27;

        }




        positionX = 1004;
        positionY = 725;        


        for(int i = 0; i < 33; ++i)
        {
    
            forPrintRectangleHorizontal(positionX, positionY);
            positionX += 27;

        }




        positionX = 1004;
        positionY = 800;        


        for(int i = 0; i < 33; ++i)
        {
    
            forPrintRectangleHorizontal(positionX, positionY);
            positionX += 27;

        }
        ///////////////////////////////////////////////////////////////////
        // END
        ///////////////////////////////////////////////////////////////////
      

    }


}





