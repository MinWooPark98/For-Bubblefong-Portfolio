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
            INIT,               // �ʱ�ȭ
            READY,              // ���� ��ٸ� (��Ī���)
            MATCHING_COMPLETE,  // ��Ī �Ϸ�
            START,              // ���ӽ���
            TOSS,               // ���� ���� 
            STANDBY,            // �������� �غ�Ϸ�
            TURN,               // ���Ͻ���
            PROCESS_READY,      // ����� ��ٸ�
            PROCESS,            // �� ���� ����
            GAMERESULT,         // ���ȭ��
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

        private ObscuredInt startTurnActor = 0;     // ���� ����
        private ObscuredBool startTurn;             // �� ���� 

        private ObscuredInt currentActor = 0;       // �̹��� ����
        private ObscuredInt currentTurn = 0;        // ���� ��

        private ObscuredBool photonConnected = false;

        /// <summary>
        /// ���� ���� �������
        /// </summary>
        private ObscuredBool gameEnd = false;           // ���� ����
        private ObscuredInt ranking = 0;                // 1 : �¸� / 2 : �й�
        private ObscuredInt dia = 0;                    // ȹ����̾�
        private GameEndType info = GameEndType.NONE;    // ���� / 0 : ���� / 1 : ��Ŀ�� / 2 : Ÿ�ӿ���
        // ------------------------------------

        /// <summary>
        /// ī�� ������ ����Ʈ �������
        /// </summary>
        private bool bFlipResult = false;        // �� ����
        private int flipActor = 0;               // ��� ����
        private int[] arrayFlipCard = null;      // ��� ī��
        private bool flipMatchingPair = false;   // ��� ��Ī
        private int flipScore = 0;               // ��� ����
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
                // ���� �˾� ����
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
