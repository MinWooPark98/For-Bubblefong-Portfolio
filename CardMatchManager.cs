using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace CardMatch
{
    public partial class CardMatchManager : MonoBehaviour
    {
        public enum CARDMATCH_STATE
        {
            NONE = -1,
            INIT,               // 초기화
            READY,              // 유저 기다림 (매칭대기)
            MATCHING_COMPLETE,  // 매칭 완료
            START,              // 게임시작
            TOSS,               // 선턴 후턴 
            STANDBY,            // 양쪽유저 준비완료
            TURN,               // 내턴시작
            PROCESS_READY,      // 상대턴 기다림
            PROCESS,            // 턴 정보 연출
            GAMERESULT,         // 결과화면
        }

        // --------------------------------------------------------------------
        // Static Instance
        // --------------------------------------------------------------------
        static CardMatchManager s_Instance = null;

        public static CardMatchManager Instance
        {
            get
            {
                return s_Instance;
            }
        }

        public static bool IsInstance()
        {
            return (s_Instance == null) ? false : true;
        }

        private CARDMATCH_STATE prevState = CARDMATCH_STATE.NONE;
        private CARDMATCH_STATE currState = CARDMATCH_STATE.NONE;
        private CARDMATCH_STATE nextState = CARDMATCH_STATE.INIT;

        [SerializeField]
        private Transform parentMainCanvas;


        private ObscuredBool gameStart = false;

        private ObscuredInt startTurnActor = 0;     // 선공 유저
        private ObscuredBool startTurn;             // 턴 시작 

        private ObscuredInt currentActor = 0;       // 이번턴 유저
        private ObscuredInt currentTurn = 0;        // 현재 턴

        private ObscuredBool photonConnected = false;

        /// <summary>
        /// 게임 종료 결과값들
        /// </summary>
        private ObscuredBool gameEnd = false;           // 게임 종료
        private ObscuredInt ranking = 0;                // 1 : 승리 / 2 : 패배
        private ObscuredInt dia = 0;                    // 획득다이아
        private GameEndType info = GameEndType.NONE;    // 사유 / 0 : 점수 / 1 : 디스커넥 / 2 : 타임오버
        // ------------------------------------

        /// <summary>
        /// 카드 뒤집은 리절트 결과값들
        /// </summary>
        private bool bFlipResult = false;        // 값 도착
        private int flipActor = 0;               // 결과 유저
        private int[] arrayFlipCard = null;      // 결과 카드
        private bool flipMatchingPair = false;   // 결과 매칭
        private int flipScore = 0;               // 결과 점수
        // -------------------------------------

        [SerializeField]
        private CardMatchUser myInfo = null;
        [SerializeField]
        private CardMatchUser enemyInfo = null;
        [SerializeField]
        private DisplayTurnTimer displayTurnTimer = null;
        [SerializeField]
        private TMP_Text textAnnouncement = null;


        [SerializeField]
        private UnityEngine.UI.Button buttonExit = null;


        private void Awake()
        {
            s_Instance = this;
        }

        private void OnDestroy()
        {
            s_Instance = null;
        }

        public void ChangeState(CARDMATCH_STATE _state)
        {
            nextState = _state;
        }

        public CARDMATCH_STATE GetState()
        {
            return currState;
        }

        private void Update()
        {
            if (PhotonManager.Instance.IsConnect())
            {
                photonConnected = true;
            }

            if (PhotonManager.Instance.IsConnect() == false && photonConnected)
            {
                PopupBasic popup = UIManager.Instance.MakePopup<PopupBasic>();
                // 에러 팝업 내용
                // popup.SetErrorAction(1);
                popup.SetAction(
                    () =>
                    {
                        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
                    });
                photonConnected = false;
            }

            if (currState != nextState)
            {
                prevState = currState;
                Exit(currState);
                currState = nextState;
                Enter(currState);
            }

            switch (currState)
            {
                case CARDMATCH_STATE.INIT:
                    UPDATE_INIT();
                    break;
                case CARDMATCH_STATE.READY:
                    UPDATE_READY();
                    break;
                case CARDMATCH_STATE.MATCHING_COMPLETE:
                    UPDATE_MATCHING_COMPLETE();
                    break;
                case CARDMATCH_STATE.START:
                    UPDATE_START();
                    break;
                case CARDMATCH_STATE.TOSS:
                    UPDATE_TOSS();
                    break;
                case CARDMATCH_STATE.STANDBY:
                    UPDATE_STANDBY();
                    break;
                case CARDMATCH_STATE.TURN:
                    UPDATE_TURN();
                    break;
                case CARDMATCH_STATE.PROCESS_READY:
                    UPDATE_PROCESS_READY();
                    break;
                case CARDMATCH_STATE.PROCESS:
                    UPDATE_PROCESS();
                    break;
                case CARDMATCH_STATE.GAMERESULT:
                    UPDATE_GAMERESULT();
                    break;
            }
        }

        private void Enter(CARDMATCH_STATE _eState)
        {
            switch (_eState)
            {
                case CARDMATCH_STATE.INIT:
                    ENTER_INIT();
                    break;
                case CARDMATCH_STATE.READY:
                    ENTER_READY();
                    break;
                case CARDMATCH_STATE.MATCHING_COMPLETE:
                    ENTER_MATCHING_COMPLETE();
                    break;
                case CARDMATCH_STATE.START:
                    ENTER_START();
                    break;
                case CARDMATCH_STATE.TOSS:
                    ENTER_TOSS();
                    break;
                case CARDMATCH_STATE.STANDBY:
                    ENTER_STANDBY();
                    break;
                case CARDMATCH_STATE.TURN:
                    ENTER_TURN();
                    break;
                case CARDMATCH_STATE.PROCESS_READY:
                    ENTER_PROCESS_READY();
                    break;
                case CARDMATCH_STATE.PROCESS:
                    ENTER_PROCESS();
                    break;
                case CARDMATCH_STATE.GAMERESULT:
                    ENTER_GAMERESULT();
                    break;
            }
        }

        private void Exit(CARDMATCH_STATE _eState)
        {
            switch (_eState)
            {
                case CARDMATCH_STATE.INIT:
                    EXIT_INIT();
                    break;
                case CARDMATCH_STATE.READY:
                    EXIT_READY();
                    break;
                case CARDMATCH_STATE.MATCHING_COMPLETE:
                    EXIT_MATCHING_COMPLETE();
                    break;
                case CARDMATCH_STATE.START:
                    EXIT_START();
                    break;
                case CARDMATCH_STATE.TOSS:
                    EXIT_TOSS();
                    break;
                case CARDMATCH_STATE.STANDBY:
                    EXIT_STANDBY();
                    break;
                case CARDMATCH_STATE.TURN:
                    EXIT_TURN();
                    break;
                case CARDMATCH_STATE.PROCESS_READY:
                    EXIT_PROCESS_READY();
                    break;
                case CARDMATCH_STATE.PROCESS:
                    EXIT_PROCESS();
                    break;
                case CARDMATCH_STATE.GAMERESULT:
                    EXIT_GAMERESULT();
                    break;
            }
        }
    }
}
