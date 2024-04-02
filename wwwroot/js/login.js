const uri = '/todo/Login';
let token = undefined;

function Login() {
    const form = document.getElementById('login');

    const body = {

        "password": document.getElementById("password").value.trim(),
        "isAdmin": false,
        "id": 0,
        "userName": document.getElementById("userName").value.trim(),
        "tasksList": []
    };
    fetch(uri, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(body),
        })
        .then(response => response.json())
        .then(data => {

            localStorage.setItem("token", data)
            location.href = "site.html"
        })
        .catch(error => console.error('error:', error));

}