class User {
    /**
     * @param {WebSocket} socket
     * @param {string} sessionId
     * @param {string} uuid
     * @param {string} nickname
     * @param {number} level
     * @param {number} exp
     */
    constructor(socket, sessionId, uuid, nickname, level, exp) {
        this.socket = socket;
        this.sessionId = sessionId;

        // 아래는 DB에서 불러올 정보
        this.uuid = uuid;
        this.nickname = nickname;
        this.level = level;
        this.exp = exp;

        this.gameUser = null;
    }
}

class UserRecord
{
    /**
     * @param {string} uuid
     */
    constructor(uuid) {
        this.uuid = uuid;

        // 아래는 DB에서 불러올 정보
    }
}

class GameUser 
{
    /**
     * @param {string} uuid
     * @param {string} nickname
     * @param {number} maxHp
     */
    constructor(uuid, nickname, maxHp) {
        this.uuid = uuid;
        this.nickname = nickname;
        this.maxHp = maxHp;
        this.hp = this.maxHp;
        this.score = 0;
        this.pos = [0, 0, 0];
        this.force = [0, 0, 0];
        this.animation = "Idle";
    }
}

exports.User = User;
exports.GameUser = GameUser;
exports.UserRecord = UserRecord;