import {
  Button,
  CardContent,
  Grid,
  Paper,
  TextField,
  Typography,
} from "@mui/material";
import { useState } from "react";
import mainLogo from "../../Assets/main-logo.png";

export default function LoginForm({ onSubmit, errorMessage }) {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [showError, setShowError] = useState({
    errorUsername: false,
    errorPassword: false,
  });

  const onFormSubmit = (event) => {
    event.preventDefault();
    if (username === "") setShowError({ ...showError, errorUsername: true });
    else if (password === "")
      setShowError({ ...showError, errorPassword: true });
    else onSubmit(username, password);
  };

  const resetErrors = () => {
    setShowError({
      errorUsername: false,
      errorPassword: false,
    });
  };
  const onUsernameChange = (value) => {
    setUsername(value);
    resetErrors();
  };

  const onPasswordChange = (value) => {
    setPassword(value);
    resetErrors();
  };

  return (
    <Paper elevation={4} className="form">
      <CardContent>
        <form noValidate onSubmit={onFormSubmit}>
          <Grid container spacing={2}>
            <Grid item xs={12} style={{ textAlign: "center" }}>
              <img src={mainLogo} height={75} width={75} alt="logo" />
            </Grid>
            <Grid item container spacing={2}>
              <Grid item xs={12}>
                <TextField
                  fullWidth
                  required
                  label="Username"
                  placeholder="Gandalf"
                  onChange={(e) => onUsernameChange(e.target.value)}
                  helperText={
                    showError.errorUsername
                      ? "Please enter a valid username"
                      : ""
                  }
                  error={showError.errorUsername}
                />
              </Grid>
            </Grid>
            <Grid item xs={12}>
              <TextField
                fullWidth
                required
                type="password"
                label="Password"
                placeholder="Password"
                onChange={(e) => onPasswordChange(e.target.value)}
                helperText={
                  showError.errorPassword ? "Please enter a valid password" : ""
                }
                error={showError.errorPassword}
              />
            </Grid>
            <Grid item xs={12}>
              <Button
                type="submit"
                fullWidth
                variant="contained"
                color="primary"
              >
                Login
              </Button>
            </Grid>
            {errorMessage && (
              <Grid item xs={12}>
                <Typography>{errorMessage}</Typography>
              </Grid>
            )}
          </Grid>
        </form>
      </CardContent>
    </Paper>
  );
}
