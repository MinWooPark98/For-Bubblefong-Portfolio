
namespace PHOTON_CARDMATCH
{
    public class EventCode
    {
        // 유저 값 설정 및 스킬을 받아옴
        public const byte OP_USER_SETTING = 10;

        // 유저 다시 기다림
        public const byte OP_WAIT_USER = 11;

        // 매칭 완료 
        public const byte OP_MATCHING_COMPLETE = 12;

        // 인게임 씬 로딩 후 시작준비 완료
        public const byte OP_READY_CEHCK = 13;

        // 게임 시작
        public const byte OP_GAME_START = 14;

        // 턴 준비 완료
        public const byte OP_STANDBY = 15

        // 턴 시작
        public const byte OP_TURN = 16;

        // 카드 뒤집음
        public const byte OP_FLIP = 17;

        // 카드 두장 뒤집었음
        public const byte OP_FLIP_PAIR = 18;

        // 카드 뒤집은 서버 결과
        public const byte OP_FLIP_RESULT = 19;

        // 게임 오버
        public const byte OP_GAME_OVER = 20;
    }

    public class USER_SETTING
    {
        public const byte ActorNumber = 0;      // 유저번호
        public const byte NickName = 1;         // 닉네임
        public const byte UserID = 2;           // 유저ID
    }

    public class READY_CHECK
    {
        // 유저 정보
    }

    public class GAME_START
    {
        // 유저 수, 카드 리스트
    }

    public class STANDBY
    {
        // 준비된 유저 정보
    }

    public class TURN
    {
        // 이번 턴 유저 정보, 초읽기 시간
    }

    public class FLIP
    {
        // 뒤집힌 카드, 유저 정보
    }

    public class FLIP_PAIR
    {
        // 뒤집힌 카드, 유저 정보
    }

    public class FLIP_RESULT
    {
        // 카드 뒤집은 결과, 유저 정보, 남은 초읽기 시간
    }

    public class GAME_OVER
    {
        // 게임 결과, 유저, 승패 사유
    }
}

