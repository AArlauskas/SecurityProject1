import { AppBar, Toolbar, Typography, IconButton } from "@mui/material";
import ExitToAppIcon from "@mui/icons-material/ExitToApp";
export default function TopBar() {
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
