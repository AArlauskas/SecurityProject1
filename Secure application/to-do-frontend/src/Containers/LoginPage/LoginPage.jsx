import { Grid, Hidden } from "@mui/material";
import loginPhoto from "../../Assets/login-logo.svg";
import LoginForm from "../../Components/LoginForm/LoginForm";
import { login } from "../../Api/Api";
import { useState } from "react";

export default function LoginPage() {
  const [errorMessage, setErrorMessage] = useState(null);
  const onLogin = (username, password) => {
    setErrorMessage(null);
    login(username, password)
      .then((response) => {
        localStorage.setItem("id", response.data.id);
        localStorage.setItem("role", response.data.role);
        localStorage.setItem("username", response.data.username);
        window.location.reload();
      })
      .catch((e) => {
        console.log(e.response);
        setErrorMessage(e.response.data);
      });
  };
  return (
    <Grid className="page" container alignContent="center" spacing={4}>
      <Hidden mdDown>
        <Grid item md={6} lg={7}>
          <img id="workerImage" src={loginPhoto} alt="login" />
        </Grid>
      </Hidden>
      <Grid item sm={11} md={6} lg={5}>
        <LoginForm onSubmit={onLogin} errorMessage={errorMessage} />
      </Grid>
    </Grid>
  );
}
