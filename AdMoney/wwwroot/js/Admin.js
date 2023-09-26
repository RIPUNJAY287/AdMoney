function AddAsset() {
    var nodeDiv = document.getElementById('assetsId');
    let col1Node = document.createElement('INPUT');
    col1Node.setAttribute('name', 'assetName');
    col1Node.setAttribute('id', 'assetName');

    let col1Label = document.createElement('label');
    col1Label.setAttribute('id', 'assetLabel');
    col1Label.innerHTML = "Asset Name";


    let col2Node = document.createElement('INPUT');
    col2Node.setAttribute('name', 'securityName');
    col2Node.setAttribute('id', 'securityName');


    let col2Label = document.createElement('label');
    col2Label.setAttribute('id', 'securityLabel');
    col2Label.innerHTML = "Security Name";

    let butNode = document.createElement('button');
    butNode.setAttribute('id', 'AddAssetBtn');
    butNode.innerHTML = "Add Asset";
    butNode.setAttribute('onclick', 'AddAssetSecurity()')

    nodeDiv.appendChild(document.createElement('br'));
    nodeDiv.appendChild(document.createElement('br'));

    nodeDiv.appendChild(col1Label);
    nodeDiv.appendChild(col1Node);
    
    nodeDiv.appendChild(document.createElement('br'));
    nodeDiv.appendChild(document.createElement('br'));
    nodeDiv.appendChild(col2Label);
    nodeDiv.appendChild(col2Node);
    
    nodeDiv.appendChild(document.createElement('br'));
    nodeDiv.appendChild(document.createElement('br'));

    nodeDiv.appendChild(butNode);


}
function AddAssetSecurity() {
    let asset = document.getElementById('assetName').value;
    let security = document.getElementById('securityName').value;
    let assetData = {
       
        "Asset": asset,
        "SecurityName" : security
    }
    try {
        $.ajax({
            url: "https://localhost:7221/Admin/AddAssetSecurities",
            type: 'POST',
            // added data type
            data: JSON.stringify(assetData),
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Headers": "*",
                //    "Access-Control-Request-Method" : "*",
                "Accept": "*",
                "Content-Type": "application/json"
            },
            success: function (res, status, xhr) {
                // alert("hello " + res.getAllResponseHeaders());

                window.location.href = 'https://localhost:7221/Admin/AdminInfo';

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

function tableToCSV() {

    // Variable to store the final csv data
    var csv_data = [];

    // Get each row data
    var rows = document.getElementsByTagName('tr');
    for (var i = 0; i < rows.length; i++) {

        // Get each column data
        var cols = rows[i].querySelectorAll('td,th');

        // Stores each csv row data
        var csvrow = [];
        for (var j = 0; j < cols.length; j++) {

            // Get the text data of each cell of
            // a row and push it to csvrow
            csvrow.push(cols[j].innerHTML);
        }

        // Combine each column value with comma
        csv_data.push(csvrow.join(","));
    }
    // combine each row data with new line character
    csv_data = csv_data.join('\n');

    /* We will use this function later to download
    the data in a csv file downloadCSVFile(csv_data);

    */
    downloadCSVFile(csv_data);
}
function downloadCSVFile(csv_data) {

    // Create CSV file object and feed our
    // csv_data into it
    CSVFile = new Blob([csv_data], { type: "text/csv" });

    // Create to temporary link to initiate
    // download process
    var temp_link = document.createElement('a');

    // Download csv file
    temp_link.download = "Admoney.csv";
    var url = window.URL.createObjectURL(CSVFile);
    temp_link.href = url;

    // This link should not be displayed
    temp_link.style.display = "none";
    document.body.appendChild(temp_link);

    // Automatically click the link to trigger download
    temp_link.click();
    document.body.removeChild(temp_link);
}