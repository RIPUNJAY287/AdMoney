$(document).ready(() => {
    alert("ehlloe");
});
function GetClient(elem) {
    alert(elem.id);
}

function AddWeightage() {
    console.log(document.getElementById("divAssets"));
    console.log(document.getElementById("divAssets").getElementsByTagName('Div'));
    console.log("hello");
    let assetDivNode = document.getElementById("divAssets").getElementsByTagName('Div');
    let tableDivNode = document.getElementById('WeightageTable');

    console.log(assetDivNode.length);
    console.log(assetDivNode[0]);
    console.log("not hello");
    let tblbody = document.createElement('tbody');
    for (let index = 0; index < assetDivNode.length; index++) {
        let inputNode = assetDivNode[index].getElementsByTagName('INPUT')[0];
        console.log(inputNode);
        console.log(inputNode.checked);
        if (inputNode.checked == true) {
            let row = document.createElement('tr');
            let cell1 = document.createElement('td');
            cell1.innerHTML = inputNode.value;
            let cell2 = document.createElement('td');
            cell2.innerHTML = '<input type = "Number" id = "inputNode.id" > </input>'

            row.appendChild(cell1);
            row.appendChild(cell2);
        }
        tblbody.append(row);
    }
    tableDivNode.appendChild(tblbody);
}