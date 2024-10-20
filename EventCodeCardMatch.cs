
namespace PHOTON_CARDMATCH
{
    public class EventCode
    {
        // ���� �� ���� �� ��ų�� �޾ƿ�
        public const byte OP_USER_SETTING = 10;

        // ���� �ٽ� ��ٸ�
        public const byte OP_WAIT_USER = 11;

        // ��Ī �Ϸ� 
        public const byte OP_MATCHING_COMPLETE = 12;

        // �ΰ��� �� �ε� �� �����غ� �Ϸ�
        public const byte OP_READY_CEHCK = 13;

        // ���� ����
        public const byte OP_GAME_START = 14;

        // �� �غ� �Ϸ�
        public const byte OP_STANDBY = 15

        // �� ����
        public const byte OP_TURN = 16;

        // ī�� ������
        public const byte OP_FLIP = 17;

        // ī�� ���� ��������
        public const byte OP_FLIP_PAIR = 18;

        // ī�� ������ ���� ���
        public const byte OP_FLIP_RESULT = 19;

        // ���� ����
        public const byte OP_GAME_OVER = 20;
    }

    public class USER_SETTING
    {
        public const byte ActorNumber = 0;      // ������ȣ
        public const byte NickName = 1;         // �г���
        public const byte UserID = 2;           // ����ID
    }

    public class READY_CHECK
    {
        // ���� ����
    }

    public class GAME_START
    {
        // ���� ��, ī�� ����Ʈ
    }

    public class STANDBY
    {
        // �غ�� ���� ����
    }

    public class TURN
    {
        // �̹� �� ���� ����, ���б� �ð�
    }

    public class FLIP
    {
        // ������ ī��, ���� ����
    }

    public class FLIP_PAIR
    {
        // ������ ī��, ���� ����
    }

    public class FLIP_RESULT
    {
        // ī�� ������ ���, ���� ����, ���� ���б� �ð�
    }

    public class GAME_OVER
    {
        // ���� ���, ����, ���� ����
    }
}

