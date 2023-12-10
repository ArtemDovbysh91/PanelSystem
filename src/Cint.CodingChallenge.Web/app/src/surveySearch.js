const initialise = () => {
    console.log('Initialising...');
    const searchForm = document.getElementById('searchForm');

    searchForm.onsubmit = (event) => {
        event.preventDefault();
        
        submitSearch();
        getSuggestions();
        return false;
    };
};

const submitSearch = () => {
    clearSearchResults();
    let text = $("#input-surveys").val();
    const request = new XMLHttpRequest();
    request.onreadystatechange = function () {
        if (request.readyState === 4 && request.status === 200) {
            renderSearchResults(JSON.parse(request.responseText));
        }
    }

    request.open("GET", `survey/search?name=${text}&number=10`);
    request.setRequestHeader("Content-Type", "application/json");
    request.send();
};

const getSuggestions = () => {
    let text = $("#input-surveys").val();
    const request = new XMLHttpRequest();
    request.onreadystatechange = function () {
        if (request.readyState == 4 && request.status == 200) {
            fillSuggestions(JSON.parse(request.responseText));
        }
    }

    request.open("GET", `survey/search?name=${text}&number=10`);
    request.setRequestHeader("Content-Type", "application/json");
    request.send();
};

const clearSearchResults = () => {
    $('ul').empty();
}

const fillSuggestions = (suggestions) => {
    $('#surveys').empty();

    for (let i in suggestions) {
        $("<option/>").html(suggestions[i].name).appendTo("#surveys");
    }
};

const renderSearchResults = (results) => {
    const searchResultsList = document.getElementById('searchResults');
    searchResultsList.childNodes = [];

    if (results) {
        for (const survey of results) {
            const resultListElement = document.createElement('li');
            resultListElement.appendChild(document.createTextNode(`Name = ${survey.name}; incentive is ${survey.incentiveEuros} EUR; length ${survey.lengthMinutes} min; `));
            resultListElement.appendChild(document.createTextNode(`description = '${survey.description}'.`));
            searchResultsList.appendChild(resultListElement);
        }
    }
};

let searchTimeout = null;
$('#input-surveys').keyup(function(e){
    let code = e.key;
    if(searchTimeout != null) clearTimeout(searchTimeout);
    if(code==="Enter"){
        clearTimeout(searchTimeout);
    } else {
        searchTimeout =setTimeout(getSuggestions,200);
    }
});

$("#input-surveys").on('input', function () {
    let val = this.value;
    if($('#surveys option').filter(function(){
        return this.value.toUpperCase() === val.toUpperCase();
    }).length) {
        //send ajax request
        submitSearch();
    }
});

module.exports = {
    initialise
};
