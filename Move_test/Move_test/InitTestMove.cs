using System;
using Microsoft.Kinect;
using System.Linq;
using System.Speech;
using System.Speech.Synthesis;
using System.Diagnostics;
//using System.IO.Ports;



namespace Move_test{

	public class InitTestMove{

		// Attribut  
		KinectSensor kinectSensor;
		const int skeletonCount = 6;
        Skeleton[] allSkeletons;
        SpeechSynthesizer synthetic;
		Move_test move;
        char c;
        Skeleton skele;
        Joint rightHand;
        Joint leftHand;
		float newDistance;
		float oldDistance;

		//Constructeur
		public InitTestMove() {
			move = new Move_test();
            synthetic = new SpeechSynthesizer();
			newDistance = 0.0f;
			oldDistance = 0.0f;
            allSkeletons = new Skeleton[skeletonCount];
		}

		//Delegate de SkeletonFrameReadyEventArgs , traite les informations
		//du squelettes si il est traqué , sinon affique une erreur.
		  private void kinect_SkeletonFrameReady (object sender, 
		                                        SkeletonFrameReadyEventArgs e)
		{ 
			// This is YOUR KinectSensor's SkeletonFrameReady event handler.
			// You do your thing here. When you want to test for a gesture match do this:


            using (SkeletonFrame frame = e.OpenSkeletonFrame())
            {

                
                if (frame == null)
                {
                    return;
                }
                else
                {
                    frame.CopySkeletonDataTo(allSkeletons);

                    skele = (from s in allSkeletons
					  where s.TrackingState == SkeletonTrackingState.Tracked ||
					   s.TrackingState == SkeletonTrackingState.PositionOnly ||
					     s.TrackingState == SkeletonTrackingState.NotTracked
					         select s).FirstOrDefault();
                    
                    if (skele != null)
                    {
						rightHand = skele.Joints[JointType.HandRight];
						leftHand = skele.Joints[JointType.HandLeft];

						if((rightHand.TrackingState == 
						    	JointTrackingState.Tracked
						   || rightHand.TrackingState == 
						    	JointTrackingState.Inferred)
						   && (leftHand.TrackingState == 
						    	JointTrackingState.Tracked
						    || leftHand.TrackingState == 
						    	JointTrackingState.Inferred)){
                                              
                        
                        newDistance = move.handPos(rightHand, leftHand);
                       
                        Console.WriteLine(c+"");
                            
                            
                           
                            if (newDistance != oldDistance)
                            {
                                c = (char)(move.moveTranslate(newDistance));
								Console.WriteLine(c+"");
                       

                            }
							oldDistance = newDistance;
                           
                        	}else{
							Console.WriteLine("Joint not tracked");
						
							}
						}else{
                        
						synthese();
                    	
						}
					}	
				}
			}
			

		
		//Méthode d'initialisation de la Kinect
		public void StartKinectST(){
			//Instancitation d'un objet kinectSensor
			kinectSensor = KinectSensor.KinectSensors[0];
   		 	kinectSensor.Start();
 
   		 	// SmoothParamater pour éliminer le bruit
  		  	TransformSmoothParameters parameters = new TransformSmoothParameters  {
   		     	 Smoothing = 0.3f,
                 Correction = 0.0f,
                 Prediction = 0.0f,
                 JitterRadius = 1.0f,
                 MaxDeviationRadius = 0.5f
    		};

  		 	 // Ouverture des flux
			kinectSensor.ColorStream.Enable();
			kinectSensor.DepthStream.Enable();

 			kinectSensor.SkeletonStream.Enable(parameters);
          	kinectSensor.SkeletonStream.OpenNextFrame(100);

		
            while (true)
            {

                kinectSensor.SkeletonFrameReady += kinect_SkeletonFrameReady;
                
            }
		}

		public void synthese(){
		
		synthetic.Resume();//relance
		
		synthetic.SetOutputToDefaultAudioDevice();
		synthetic.Speak("you are not Tracked");
		synthetic.Pause();//met en pause

			return;
		}
	}
}

