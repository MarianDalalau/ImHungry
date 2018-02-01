var witAccessToken = 'JDW5BHPZ6W24HKNKOPE7XEQPLR5MV4PR';

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
    //var r = kv("intent", intent);
    var ingredients = [];
    for (var k in entities) {
        var e = entities[k];

        if (!(e instanceof Array)) {
            //r += kv(k, e.value);
            ingredients.push(e.value);
        } else {
            for (var i = 0; i < e.length; i++) {
                //r += kv(k, e[i].value);
                ingredients.push(e[i].value);
            }
        }
    }

    RefreshRecipeResults(intent, ingredients);
    //document.getElementById("result").innerHTML = r;    
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

mic.connect(witAccessToken);

function kv(k, v) {
    if (toString.call(v) !== "[object String]") {
        v = JSON.stringify(v);
    }
    return k + "=" + v + "\n";
}

$('.wit-microphone').click(function (event) {
    //start/stop the microphone depending on user interaction
    if ($(this).hasClass('active')) {
        mic.stop();
        $('.tooltip-inner').html('Click here to start');
    }
    else {
        mic.start();
        $('.tooltip-inner').html('Click here to stop');
    }

    //add/remove the active class
    $(this).toggleClass('active');

    $('.tooltip').toggleClass('tooltip-record');
    
});


$('.searchBtn').click(function (event) {

    $.ajax({
        url: 'https://api.wit.ai/message',
        data: {
            'q': $('#search').val(),
            'access_token': witAccessToken
        },
        dataType: 'jsonp',
        method: 'GET',
        success: function (response) {
            if (response.entities) {
                //get the intent
                var intent = '';
                if (response.entities.intent && response.entities.intent.length > 0)
                    intent = response.entities.intent[0].value;
                //get the ingredients from the response
                var ingredients = [];
                if (response.entities.recipe && response.entities.recipe.length > 0) {
                    for (var i = 0; i < response.entities.recipe.length; i++) {
                        ingredients.push(response.entities.recipe[i].value);
                    }
                }

                RefreshRecipeResults(intent, ingredients);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {

        }        
    });
});

function RefreshRecipeResults(intent, ingredients)
{
    $("#recipeListContainer").addClass("loading");
    $(".results").prepend('<div class="preloader"><span>Loading recipes...</span></div>');

    var values =
        {
            "intent": intent,
            "ingredients": ingredients
        };

    $.ajax({
        url: "/Home/RefreshRecipeList/",
        data: JSON.stringify(values),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $('#recipeListContainer').html(data.recipeList);

            $("#recipeListContainer").removeClass("loading");
            $(".results div.preloader").remove();

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $("#recipeListContainer").removeClass("loading");
            $(".results div.preloader").remove();
            
        }
    });
}