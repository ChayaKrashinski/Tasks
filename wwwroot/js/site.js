const uri = '/user';

const token = localStorage.getItem('token');
const auth = `Bearer ${ token }`

fetch(uri, {
        method: 'GET',
        headers: {
            'Authorization': auth,
            'Accept': 'application/json',
            // 'Content-Type': 'application/json'
        },
    })
    // .then(response => response.json())
    .then(data => {
        if (data.ok)
            if (data == null) console.error('the data is null');
            else localStorage.setItem("user", JSON.stringify(data))
        else console.error('the data is not ok');
    })
    .catch(error => {
        console.error('error:', error)
        alert("unAuthorize!!")
            // location.href = "index.html"
    });