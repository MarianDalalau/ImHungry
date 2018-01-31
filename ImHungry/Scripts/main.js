var mic = new Wit.Microphone(document.getElementById("microphone"));
var info = function (msg) {
    document.getElementById("info").innerHTML = msg;
};
var error = function (msg) {
    document.getElementById("error").innerHTML = msg;
};
mic.onready = function () {
    info("Microphone is ready to record");
};
mic.onaudiostart = function () {
    info("Recording started");
    error("");
};
mic.onaudioend = function () {
    info("Recording stopped, processing started");
};
mic.onresult = function (intent, entities) {
    var r = kv("intent", intent);

    for (var k in entities) {
        var e = entities[k];

        if (!(e instanceof Array)) {
            r += kv(k, e.value);
        } else {
            for (var i = 0; i < e.length; i++) {
                r += kv(k, e[i].value);
            }
        }
    }

    document.getElementById("result").innerHTML = r;
    document.getElementById("serch").innerHTML = r;

};
mic.onerror = function (err) {
    error("Error: " + err);
};
mic.onconnecting = function () {
    info("Microphone is connecting");
};
mic.ondisconnected = function () {
    info("Microphone is not connected");
};

mic.connect("JDW5BHPZ6W24HKNKOPE7XEQPLR5MV4PR");
//mic.start();
// mic.stop();

function kv(k, v) {
    if (toString.call(v) !== "[object String]") {
        v = JSON.stringify(v);
    }
    return k + "=" + v + "\n";
}

$('.wit-microphone').click(function (event) {
    //start/stop the microphone depending on user interaction
    $(this).hasClass('active') ? mic.stop() : mic.start();

    //add/remove the active class
    $(this).toggleClass('active');

    $('.tooltip').toggleClass('tooltip-record');
    
});


//$.ajax({
//    url: 'https://api.wit.ai/message',
//    data: {
//        'q': 'set an alarm in 10min',
//        'access_token': 'MY_WIT_TOKEN'
//    },
//    dataType: 'jsonp',
//    method: 'GET',
//    success: function (response) {
//        console.log("success!", response);
//    }
//});