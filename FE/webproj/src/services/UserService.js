import axios from "axios";

export const LogIn = async (username, password, handleAlert, navigate) =>
{
    try{
        if(username === "" || password === "")
        {
            handleAlert("Enter both fields, username and password.", "error");
            return;
        }
        const response = await axios.post(`${process.env.REACT_APP_API_BASE_URL}/api/User/login`,
        {
            username,
            password
        }); 

        const { token, ...logedInUser } = response.data; 
        localStorage.setItem("token", token);
        localStorage.setItem("logedInUser", JSON.stringify(logedInUser));
        handleAlert("Successfully loged in. You will be redirected to dashboard in a seconds.", "success");
        setTimeout(() => {navigate("/dashboard");}, 3000);
        return response;
    }
    catch(ex)
    {
        console.error("Error while trying to log in: ", ex.response.data.message);
        handleAlert(ex.response.data.message, "error");
        return ex.response;
    }
}

export const FacebookLogIn = async (fullname, id, pictureUrl, email, handleAlert, navigate) =>
{
    try{

        const response = await axios.post(`${process.env.REACT_APP_API_BASE_URL}/api/User/facebooklogin`,
        {
            fullname, id, pictureUrl, email
        });
        
        const {token, ...logedInUser } = response.data;
        localStorage.setItem("token", token);
        localStorage.setItem("logedInUser", JSON.stringify(logedInUser));
        handleAlert("Successfully loged in. You will be redirected to dashboard in a seconds.", "success");
        setTimeout(() => {navigate("/dashboard");}, 3000);
        return response;
    }
    catch(ex)
    {
        console.error("Error while trying to log in with facebook: ", ex.response.data.message);
        handleAlert(ex.response.data.message, "error");
        return ex.response;
    }
}

export const RegisterUser = async (username, 
    email, 
    password,
    passwordRepeat, 
    name, 
    lastname, 
    dateOfBirth, 
    adress, 
    userType,
    profilePicture,
    handleAlert,
    navigate) =>
{
    try{   
        if(username === "" || email === "" || password === "" || passwordRepeat === "" || name === "" || lastname === "" || dateOfBirth === "" || adress === "" || userType === "")
        {
            handleAlert("All fields are mandatory.", "error");
            return;
        }
        if(password !== passwordRepeat)
        {
            handleAlert("Passwords does not match.", "error");
            return;
        } 

        const formData = new FormData();
        formData.append("username", username);
        formData.append("email", email);
        formData.append("password", password);
        formData.append("passwordRepeat", passwordRepeat);
        formData.append("name", name);
        formData.append("lastname", lastname);
        formData.append("dateOfBirth", dateOfBirth);
        formData.append("adress", adress);
        formData.append("userType", userType);
        formData.append("profilePicture", profilePicture);



        const response = await axios.post(
            `${process.env.REACT_APP_API_BASE_URL}/api/User`, 
            formData);

        handleAlert("Successfully registered, you can now log in.", "success");
        setTimeout(() => {navigate("/login");}, 3000);
        return response;
    }
    catch(ex)
    {
        console.error("Error while trying to register: ", ex.response.data.message);
        handleAlert(ex.response.data.message, "error");
        return ex.response;
    }
}