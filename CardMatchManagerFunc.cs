using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardMatch
{
    public partial class CardMatchManager : MonoBehaviour
    {
        public void GameStart()
        {
            gameStart = true;
        }

        public void Exit()
        {
            PhotonManager.Instance.LeaveRoom();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
        }

        public Transform GetParentMainCanvas()
        {
            return parentMainCanvas;
        }

        public void SetUserInfo(int _actorNumber, string _nickname, int _characterID)
        {
            if (_actorNumber == PhotonManager.Instance.GetMyActorNumber())
            {
                myInfo.SetActorNumber(_actorNumber);
                myInfo.SetNickname(_nickname);
                myInfo.SetCharacterID(_characterID);
            }
            else
            {
                enemyInfo.SetActorNumber(_actorNumber);
                enemyInfo.SetNickname(_nickname);
                enemyInfo.SetCharacterID(_characterID);
            }
        }

        /// <summary>
        /// 선공 유저 설정
        /// </summary>
        /// <param name="_nActorNumber"></param>
        public void SetStartTurnActor(int _nActorNumber)
        {
            startTurnActor = _nActorNumber;
        }

        /// <summary>
        /// 턴 시작
        /// </summary>
        /// <param name="_nActorNumber"></param>
        /// <param name="_bStartTurn"></param>
        public void SetStartTurn(int _nActorNumber, bool _bStartTurn)
        {
            currentActor = _nActorNumber;
            startTurn = _bStartTurn;
        }

        /// <summary>
        /// 턴 시작 되었나?
        /// </summary>
        /// <returns></returns>
        public bool IsStartTurn()
        {
            return startTurn;
        }

        /// <summary>
        /// 현재 턴
        /// </summary>
        /// <param name="_nTurn"></param>
        public void SetCurrentTurn(int _nTurn, bool _bTurnChange)
        {
            currentTurn = _nTurn;
            displayTurnTimer.SetTurnCount(_nTurn);
        }

        /// <summary>
        /// 현재 턴
        /// </summary>
        /// <returns></returns>
        public int GetCurrentTurn()
        {
            return currentTurn;
        }

        /// <summary>
        /// 현재 턴 유저
        /// </summary>
        /// <returns></returns>
        public int GetCurrentActor()
        {
            return currentActor;
        }

        /// <summary>
        /// 포톤에서 카드 뒤집은 정보 받음
        /// </summary>
        /// <param name="_nActorNumber"></param>
        /// <param name="_nIdx"></param>
        public void FlipCardData(int _nActorNumber, int _nIdx)
        {
            CardDataManager.Instance.FlipOnCard(_nIdx);
        }

        /// <summary>
        /// 포톤에서 뒤집은 결과값 받음
        /// </summary>
        /// <param name="_actorNumber"></param>
        /// <param name="_arrayCard"></param>
        /// <param name="_matchingPair"></param>
        /// <param name="_score"></param>
        /// <param name="_bGameEnd"></param>
        public void FlipResultData(int _actorNumber, int[] _arrayCard, bool _matchingPair, int _score)
        {
            flipResult = true;
            flipActor = _actorNumber;
            arrayFlipCard = _arrayCard;
            flipMatchingPair = _matchingPair;
            flipScore = _score;
        }

        public void SetTimeTurn(int _actorNumber, float _timeTurn)
        {
            if (_actorNumber == PhotonManager.Instance.GetMyActorNumber())
            {
                myInfo.SetTimeTurn(_timeTurn);
            }
            else
            {
                enemyInfo.SetTimeTurn(_timeTurn);
            }
        }

        public void SetTimeOver(int _actorNumber, float _timeOver)
        {
            if (_actorNumber == PhotonManager.Instance.GetMyActorNumber())
            {
                myInfo.SetTimeOver(_timeOver);
            }
            else
            {
                enemyInfo.SetTimeOver(_timeOver);
            }
        }

        public void GameEnd(int _actorNumber, int _ranking, int _dia, GameEndType _info)
        {
            if (_actorNumber != PhotonManager.Instance.GetMyActorNumber())
            {
                return;
            }

            gameEnd = true;

            ranking = _ranking;
            dia = _dia;
            info = _info;

            if (info != GameEndType.SCORE)
            {
                if (GetState() == CARDMATCH_STATE.MATCHING_COMPLETE || GetState() == CARDMATCH_STATE.START || GetState() == CARDMATCH_STATE.TOSS)
                {

                }
                else
                {
                    ChangeState(CARDMATCH_STATE.GAMERESULT);
                }
            }
        }
    }
}
