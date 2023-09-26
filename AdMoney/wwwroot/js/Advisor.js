$(document).ready(() => {
    if (document.getElementById("riskProfile") != null) {
        document.getElementById("riskProfile").style.display = "none";
    }

    if (document.getElementById("PieChartBody") != null) {
        document.getElementById("PieChartBody").style.display = "none";
    }
    //alert("ehlloe");

    if (document.getElementById("NextQuestionId") != null) {
        document.getElementById("NextQuestionId").addEventListener("click", (e) => {
            e.preventDefault();

            var quesId = Number(document.getElementById("questionId").value);
            var clientId = Number(document.getElementById("clientId").value);
            var options = document.getElementById('radioOptions').getElementsByTagName('INPUT');
            let checkedoption = 0;
            for (let index = 0; index < options.length; index++) {
                console.log(options[index].checked);
                if (options[index].checked == true) {
                    console.log(options[index]);
                    checkedoption = Number(options[index].id);
                }
            }
            console.log(quesId + " " + clientId + " " + checkedoption);
            if (checkedoption == 0) {
                return;
            }

            var obj = {
                "ClientId": clientId,
                "QuestionId": quesId,
                "OptionId": checkedoption
            }
            console.log(obj);
            try {
                $.ajax({
                    url: "https://localhost:7221/Advisor/NextQuestion",
                    type: 'POST',
                    // added data type
                    data: JSON.stringify(obj),
                    headers: {
                        "Access-Control-Allow-Origin": "*",
                        "Access-Control-Allow-Headers": "*",
                        //    "Access-Control-Request-Method" : "*",
                        "Accept": "*",
                        "Content-Type": "application/json"
                    },
                    success: function (res, status, xhr) {
                        // alert("hello " + res.getAllResponseHeaders());
                        res = JSON.parse(res);
                        console.log(res);
                        console.log(JSON.parse(xhr.responseText));
                        if (res.NextQuestion == true) {
                            document.getElementById("questionId").value = res.QuestionForAdClient.QuestionId;
                            document.getElementById("question").value = res.QuestionForAdClient.Question;

                            var options = document.getElementById('radioOptions');
                            options.innerHTML = "";
                            for (let index = 0; index < res.QuestionForAdClient.Options.length; index++) {
                                var node = document.createElement('Div');
                                node.setAttribute("class", "form-check");
                                node.innerHTML = "<input class='form-check-input' type='radio' name='flexRadioDefault' id='" + res.QuestionForAdClient.OptionId[index] + "'>" + res.QuestionForAdClient.Options[index];

                                options.appendChild(node);
                            }
                        }
                        else {



                            document.getElementById('questionForm').style.display = "none";
                            document.getElementById('riskProfile').style.display = "block";
                            document.getElementById('riskPic').setAttribute("src", "../Static/" + res.RiskProfile + ".png");
                            document.getElementById('profile').innerHTML = res.RiskProfile;


                        }

                    },
                    error: function (er) {
                        alert("error");
                        alert(JSON.stringify(er));
                    }

                });
            }
            catch (e) {
                console.log(e);
            }


        });
    }







    if (document.getElementById("AddModelForClients") != null) {
        document.getElementById("AddModelForClients").addEventListener("click", (e) => {
            e.preventDefault();


            var modelDiv = document.getElementById('radioOptionsForModel').getElementsByTagName('Div');
            let modId = 0;
            for (let index = 0; index < modelDiv.length; index++) {
                let modelIn = modelDiv[index].getElementsByTagName('INPUT')[0];
                if (modelIn.checked) {
                    modId = modelIn.id;
                }
            }
            if (modId == 0) {
                return;
            }
            var modelData = {
                "clientId": document.getElementById('clientId').value,
                "modelId": modId
            }
            try {
                $.ajax({
                    url: "https://localhost:7221/Advisor/AddModelToClient",
                    type: 'POST',
                    // added data type
                    data: JSON.stringify(modelData),
                    headers: {
                        "Access-Control-Allow-Origin": "*",
                        "Access-Control-Allow-Headers": "*",
                        //    "Access-Control-Request-Method" : "*",
                        "Accept": "*",
                        "Content-Type": "application/json"
                    },
                    success: function (res, status, xhr) {
                        // alert("hello " + res.getAllResponseHeaders());

                        console.log(xhr);
                        window.location.href = 'https://localhost:7221/Advisor/AdvisorProfile';

                    },
                    error: function (er) {
                        alert("error");
                        alert(JSON.stringify(er));
                    }

                });
            }
            catch (e) {
                console.log(e);
            }


        });

    }



});
function GetClient(elem) {
    alert(elem.id);
}

function AddWeightage() {
    console.log(document.getElementById("divAssets"));
    console.log(document.getElementById("divAssets").getElementsByTagName('Div'));
    let assetDivNode = document.getElementById("divAssets").getElementsByTagName('Div');
    let tableDivNode = document.getElementById('WeightageTable');
    let tblbody = document.createElement('tbody');
    tblbody.setAttribute('id','WeightTBody')
    let row = document.createElement('tr');
    cell1 = document.createElement('th');
    cell2 = document.createElement('th');
    cell1.innerHTML = 'Asset-Security'
    cell2.innerHTML = 'Weightage';
    row.appendChild(cell1);
    row.appendChild(cell2);
    tblbody.append(row);


    for (let index = 0; index < assetDivNode.length; index++)
    {
        console.log(index);
        let inputNode = assetDivNode[index].getElementsByTagName('INPUT')[0];
        console.log(inputNode);
        console.log(inputNode.checked);
         row = document.createElement('tr');
        if (inputNode.checked == true)
        {
          
            cell1 = document.createElement('td');
            cell1.innerHTML = assetDivNode[index].id;
            cell2 = document.createElement('td');
            cell2.innerHTML = '<input type = "Number" id = '+inputNode.id+' > </input>'

            row.appendChild(cell1);
            row.appendChild(cell2);
        }
        tblbody.append(row);
    }
    tableDivNode.appendChild(tblbody);
    let butn = document.createElement('button');
    butn.setAttribute('type', 'submit');
    butn.setAttribute('onclick', 'AddModel()');
    butn.innerHTML = "Add Model";
    document.getElementById('ModelDiv').append(butn);
}

function AddModel() {
    let weightVal = document.getElementById('WeightTBody').getElementsByTagName('td');
    let riskModel = document.getElementById('modelRole').value;
    var data = new Array();
    for (let index = 0; index < weightVal.length ; index+=2)
    {
        let weightName = weightVal[index].innerHTML.split('-');
        
        let val = {
            "Id": weightVal[index + 1].getElementsByTagName('input')[0].id,
            "Weight": weightVal[index + 1].getElementsByTagName('input')[0].value,
        }
        data.push(val);
    }
    /*var modelData = {
        data = data,
        modelRisk = riskModel
    }*/
    console.log(data)

    var modelData = { "modelInput": data , "risk" : riskModel};

    try {
        $.ajax({
            url:"https://localhost:7221/Advisor/AddModel",
            type: 'POST',
            // added data type
            data: JSON.stringify(modelData),
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Headers": "*",
                //    "Access-Control-Request-Method" : "*",
                "Accept": "*",
                "Content-Type": "application/json"
            },
            success: function (res, status, xhr) {
                // alert("hello " + res.getAllResponseHeaders());

                console.log(xhr);
                window.location.href = 'https://localhost:7221/Advisor/AdvisorProfile';

            },
            error: function (er) {
                alert("error");
                alert(JSON.stringify(er));
            }

        });
    }
    catch (e)
    {
        console.log(e);
    }
    console.log(data);
}

function GetRiskData(clientId) { 
     console.log(clientId + " erererre");
    var obj = {
        "ClientId": clientId,
        "QuestionId": 0,
        "OptionId": 0
    }
    console.log(obj);
    try {
        $.ajax({
            url: "https://localhost:7221/Advisor/NextQuestion",
            type: 'POST',
            // added data type
            data: JSON.stringify(obj),
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Headers": "*",
                //    "Access-Control-Request-Method" : "*",
                "Accept": "*",
                "Content-Type": "application/json"
            },
            success: function (res, status, xhr) {
                // alert("hello " + res.getAllResponseHeaders());
                res = JSON.parse(res);
                console.log(res);
                console.log(JSON.parse(xhr.responseText));
                if (res.NextQuestion == true) {
                    document.getElementById("questionId").value = res.QuestionForAdClient.QuestionId;
                    document.getElementById("question").value = res.QuestionForAdClient.Question;

                    var options = document.getElementById('radioOptions');
                    options.innerHTML = "";
                    for (let index = 0; index < res.QuestionForAdClient.Options.length; index++) {
                        var node = document.createElement('Div');
                        node.setAttribute("class", "form-check");
                        node.innerHTML = "<input class='form-check-input' type='radio' name='flexRadioDefault' id='" + res.QuestionForAdClient.OptionId[index] + "'>" + res.QuestionForAdClient.Options[index];

                        options.appendChild(node);
                    }
                }
                else {
                    document.getElementById('riskProfile').style.display = "block";
                    document.getElementById('riskPic').setAttribute("src", "../Static/" + res.RiskProfile + ".png");
                    document.getElementById('profile').innerHTML = res.RiskProfile;
                }

            },
            error: function (er) {
                alert("error");
                alert(JSON.stringify(er));
            }

        });
    }
    catch (e) {
        console.log(e);
    }
}

function modalClose() {
    if (document.getElementById("PieChartBody") != null) {
        document.getElementById("PieChartBody").style.display = "none";
    }
}
function GetPieChart(htmlVal) {

    console.log(htmlVal);
    document.getElementById("PieChartBody").style.display = "block";
    console.log(htmlVal.getElementsByTagName('p'));
    let pNodes = htmlVal.getElementsByTagName('p');
    let hNodes = htmlVal.getElementsByTagName('h3');
    
 
    var data = { labels: [], weightage: [] }
    for (var i = 0; i < pNodes.length; i++) {
        data.labels.push(pNodes[i].innerHTML)
        data.weightage.push(Number(hNodes[i].innerHTML));
    }
    
    document.getElementById("pieChart").innerHTML = "";
    var canvasP = document.getElementById('pieChart')
    var ctxP = canvasP.getContext('2d')
    var myPieChart = new Chart(ctxP, {
        type: 'pie',
        data: {
            labels: data.labels,
            datasets: [
                {
                    data: data.weightage,
                    backgroundColor: [
                        '#64B5F6',
                        '#FFD54F',
                        '#2196F3',
                        '#FFC107',
                        '#1976D2',
                        '#FFA000',
                        '#0D47A1'
                    ],
                    hoverBackgroundColor: [
                        '#B2EBF2',
                        '#FFCCBC',
                        '#4DD0E1',
                        '#FF8A65',
                        '#00BCD4',
                        '#FF5722',
                        '#0097A7'
                    ]
                }
            ]
        },
        options: {
            legend: {
                display: true,
                position: 'right'
            }
        }
    });
}