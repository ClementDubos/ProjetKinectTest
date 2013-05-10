
using System;
using System.Threading;
using System.Linq;
using Microsoft.Kinect;
using System.Speech;
using System.Speech.Recognition;
using System.Speech.AudioFormat;
using System.Speech.Recognition.SrgsGrammar;
using System.Windows.Threading;
using System.IO;
using Kinect.Toolbox.Voice;
using Kinect.Toolbox;
using System.Diagnostics;
using System.IO.Ports;



public class MainClass{

	//Attribut
	KinectSensor sensor;
    bool etat = false;
    //Stopwatch timeElaps = new Stopwatch() ;
    int i=0;
    //SerialPort port=new SerialPort("COM3",9600);
    char c;
	//Requette

	//Constructeur



   /* public SerialPort getPort()
    {

        return port;
    }*/

	static void Main (string[] args)
	{
        
		MainClass mClass = new MainClass ();
		mClass.sensor = KinectSensor.KinectSensors [0];
		mClass.sensor.Start ();
		VoiceCommander voiceCommander = new VoiceCommander ("start", "close", "move");
        voiceCommander.OrderDetected += mClass.voiceCommander_OrderDetected;
            
		voiceCommander.Start (mClass.sensor);
       // mClass.getPort().Open();
        while (true)
        {
            
        }
	}

	void voiceCommander_OrderDetected (string order)
	{
        
        
        
		switch (order) {
                
		case "start":
                if (!etat)
                {
                    etat=true;
                    //port.Write("a");
                    Console.WriteLine("a");
                    
                }
			break;
		case "close":
            if (etat)
            {
                etat = false;
               // port.Write("z");
                Console.WriteLine("z");
            }
			break;
		case "move":
            if (etat)
            {
               // while (timeElaps.ElapsedMilliseconds <= 5000)
                while (i <= 11)
                {
                    i++;
                   // timeElaps.Start();
                    c = (char)(96 + i);
                    Console.WriteLine(i + "");
                   // port.Write(""+c);
                 //   timeElaps.Stop();
                }
            }
            
                i=0;
			break;
		default:
			break;
		}
	}
}

