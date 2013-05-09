using System;
using Microsoft.Kinect;


namespace Move_test
{
	public class Move_test
	{


		//Attribut
		float distance;
		int ascii;
		float rightX;
		float rightY;
		float rightZ;
		float leftX;
		float leftY;
		float leftZ;
		float rlX;
		float rlY;
		float rlZ;
		int i;
		float m;
		//Constructeur
		
		public Move_test () {
			ascii = 0;
		}

	

		//Methode handPos() qui recupere la distance entre les deux mains
		public float handPos (Joint rightHand , Joint leftHand){


    			// recupere les point respectif de la main droite
				// et la main gauche
    			rightX = rightHand.Position.X;
    			rightY = rightHand.Position.Y;
    			rightZ = rightHand.Position.Z;
				leftX = leftHand.Position.X;
				leftY = leftHand.Position.Y;
				leftZ = leftHand.Position.Z;

			rlX =(float)Math.Pow((rightX - leftX),2);
			rlY =(float)Math.Pow((rightY - leftY),2);
			rlZ =(float)Math.Pow((rightZ - leftZ),2);
			distance =(float)Math.Sqrt(rlX+rlY+rlZ);
			// Transforme les coordonnées pour simplifer le calcul de Distance
			return distance;

			//Bloque de traitement de la distance pour l'éclairage.


		}
		//traduit la distance en une valeur ascii pour l'Arduino
		public int moveTranslate(float a){



			//si la distance est supérieur a 1 cm
			if(a > 1){

				//on teste les cas jusqu'a 1 m
				for( i = 20 ; i <= 100 ; i = i+10){
					m =i/100;
					//si cette valeur est inferieur ou egale a m;
					if(m.CompareTo(a)>= 0){

						//on effectue l'attribution.
						ascii = 96+i/10;
						return ascii ;
					}
				}
				//si elle est supérieur a 1m
				//Les Led sont toute allumé
				ascii = 106;
				return ascii;
			} else {
				//sinon , elle est inferieur a 1
				//et elles sont toute eteinte.
				ascii = 97;
				return ascii;
			}
		
		

	}
}

}
