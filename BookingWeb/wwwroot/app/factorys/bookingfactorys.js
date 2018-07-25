app.factory('TimeData', function ($rootScope) {
    var time = "";

    return {
        set: set,
        get: get
    };

    function set(item) {
        this.time = item;
        $rootScope.$emit("timeEvent");
    }

    function get() {
        return this.time;
    }
});

app.factory('DayData', function ($rootScope) {
    var day = "";

    return {
        set: set,
        get: get
    };

    function set(item) {
        this.day = item;
        $rootScope.$emit("dayEvent");
    }

    function get() {
        return this.day;
    }
});

app.factory('ReloadData', function ($rootScope) {
    var reload = "";

    return {
        set: set,
        get: get
    };

    function set(item) {
        this.reload = item;
        $rootScope.$emit("reloadEvent");
    }

    function get() {
        return this.reload;
    }
});

app.factory('LoginData', function ($rootScope) {
    var login = { "id": "", "status": false };

    return {
        set: set,
        get: get
    };

    function set(item) {
        this.login = item;
        $rootScope.$emit("loginEvent");
    }

    function get() {
        return this.login;
    }
});