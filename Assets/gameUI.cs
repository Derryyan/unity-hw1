using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameUI : MonoBehaviour {
		enum Player {non, player1, player2};
		bool whose_turn = true;
		bool winner;
		int count = 0;
		Player[,] playarea = new Player[3, 3];
		void Start() {
			winner = false;
			Reset();
		}
    void OnGUI() {
				winner = Countinue();
				float bwidth = 100;
				float bheight = 100;
				float x = (Screen.width - bwidth)*0.5f;
				float y = (Screen.height - bheight)*0.5f;
				if (GUI.Button(new Rect(50,y,200,20),"Click here to reset")) {
					Reset();
				}
      if (winner) {
				for (int i = 0;i < 3;i++) {
					for (int j = 0;j < 3;j++) {
						float pressX = x + (i-1)*100;
						float pressY = y + (j-1)*100;
						if (playarea[i,j] == Player.non) {
							if (GUI.Button(new Rect(pressX,pressY,bwidth,bheight),"")) {
								if (whose_turn) {
									playarea[i,j] = Player.player1;
								} else playarea[i,j] = Player.player2;
								whose_turn = !whose_turn;
								count++;
							}
						}
						if (playarea[i,j] == Player.player1) {
							GUI.Button(new Rect(pressX,pressY,bwidth,bheight),"O");
						}
						if (playarea[i,j] == Player.player2) {
							GUI.Button(new Rect(pressX,pressY,bwidth,bheight),"X");
						}
					}
				}
			}else {
				for (int i = 0;i < 3;i++) {
					for (int j = 0;j < 3;j++) {
						float pressX = x + (i-1)*100;
						float pressY = y + (j-1)*100;
						if (playarea[i,j] == Player.non) {
							GUI.Button(new Rect(pressX,pressY,bwidth,bheight),"");
						}
						if (playarea[i,j] == Player.player1) {
							GUI.Button(new Rect(pressX,pressY,bwidth,bheight),"O");
						}
						if (playarea[i,j] == Player.player2) {
							GUI.Button(new Rect(pressX,pressY,bwidth,bheight),"X");
						}
					}
				}
			}
			GUIStyle setFont = new GUIStyle();
			setFont.fontSize = 40;
			string playing = ((whose_turn)? "player1":"player2");
			string winnerName = ((!whose_turn)? "player1":"player2");//获胜者必然是上一回合的玩家（因为下完一步就自动换成另一个玩家，所以是上一回合而不是这回合的玩家获胜）
			if (winner&& count != 9) {
				GUI.Label(new Rect(x-100,50,bwidth,bheight),"Now "+ playing + " playing",setFont);
			}
			if (winner&& count == 9) {
				GUI.Label(new Rect(x-100,50,bwidth,bheight),"Draw",setFont);
			}
			if (!winner) {
				GUI.Label(new Rect(x-100,50,bwidth,bheight),winnerName + " win",setFont);
			}
    }
		void Reset() {
			for (int i = 0;i<3;i++) {
				for(int j=0;j<3;j++) {
					playarea[i,j] = Player.non;
				}
			}
			count = 0;
			whose_turn = true;
		}
		bool Countinue() {
			for (int i = 0;i < 3;i++) {
				if ((playarea[0,i] != Player.non && playarea[0,i] == playarea[1,i] && playarea[1,i] == playarea[2,i])||(playarea[i,0] != Player.non && playarea[i,0] == playarea[i,1] && playarea[i,1] == playarea[i,2])
				) {
					return false;
				}
				if ((playarea[0,0]!= Player.non && playarea[0,0]== playarea[1,1] && playarea[1,1]== playarea[2,2])||(playarea[2,0]!= Player.non && playarea[2,0]== playarea[1,1] && playarea[1,1]== playarea[0,2])) {
					return false;
				}
			}
			return true;
		}
}
