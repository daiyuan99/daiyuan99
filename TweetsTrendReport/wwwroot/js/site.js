var protocol = location.protocol;
var slashes = protocol.concat("//");
var hostUrl = slashes.concat(window.location.host) + "/";

var apiUrlBase = hostUrl + "api/TwitterSampleStream/";

function StartStream()
{
    $.get(apiUrlBase + 'Start', function (data) {
        document.getElementById('myMessageBoard').innerHTML = data;
    });
}

function StopStream() {
    $.get(apiUrlBase + 'Stop', function (data) {
        document.getElementById('myMessageBoard').innerHTML = data;
    });
}

function ShowTweetTrend() {
    $.get(apiUrlBase + 'TweetTrend', function (data) {
        document.getElementById('myMessageBoard').innerHTML = data;
    });
}

function ShowLastError() {
    $.get(apiUrlBase + 'Errors', function (data) {
        document.getElementById('myMessageBoard').innerHTML = data;
    });
}
function ShowLastTweet() {
    $.get(apiUrlBase + 'LastTweet', function (data) {
        document.getElementById('myMessageBoard').innerHTML = data;
    });
}