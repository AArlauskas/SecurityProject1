import { AppBar, Toolbar, Typography, IconButton, Button } from "@mui/material";
import ExitToAppIcon from "@mui/icons-material/ExitToApp";
import { useHistory } from "react-router";
import PATH from "../../Contstants/Path";
export default function TopBar() {
  const history = useHistory();
  const onLogout = () => {
    window.localStorage.clear();
    window.location.reload();
  };
  return (
    <AppBar position="static">
      <Toolbar>
        <Typography variant="h6" noWrap component="div" sx={{ flexGrow: 1 }}>
          To Do Application
        </Typography>
        {window.localStorage.getItem("role") === "admin" && (
          <Button
            color="secondary"
            variant="contained"
            onClick={() => history.push(PATH.ADMIN)}
            style={{ marginRight: 10 }}
          >
            Admin
          </Button>
        )}
        <Typography variant="subtitle1" noWrap>
          {window.localStorage.getItem("username")}
        </Typography>
        <IconButton onClick={onLogout}>
          <ExitToAppIcon style={{ color: "white" }} />
        </IconButton>
      </Toolbar>
    </AppBar>
  );
}
