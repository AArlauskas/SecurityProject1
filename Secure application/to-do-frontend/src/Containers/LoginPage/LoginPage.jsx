import { Grid, Hidden } from "@mui/material";
import loginPhoto from "../../Assets/login-logo.svg";
import LoginForm from "../../Components/LoginForm/LoginForm";
import { useState } from "react";
import { authenticate, getCurrentUser } from "../../Api/PublicApi";

export default function LoginPage() {
  const [errorMessage, setErrorMessage] = useState(null);
  const onLogin = (username, password) => {
    setErrorMessage(null);
    authenticate(username, password)
      .then((tokenResponse) => {
        getCurrentUser(tokenResponse.data)
          .then((userResponse) => {
            const { data } = userResponse;
            localStorage.setItem("token", tokenResponse.data);
            localStorage.setItem("id", data.id);
            localStorage.setItem("role", data.role);
            localStorage.setItem("username", data.username);
            window.location.reload();
          })
          .catch(() => setErrorMessage("User not found or wrong credentials"));
      })
      .catch(() => setErrorMessage("User not found or wrong credentials"));
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
