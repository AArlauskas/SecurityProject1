import {
  BrowserRouter as Router,
  Redirect,
  Route,
  Switch,
} from "react-router-dom";
import LoginPage from "./Containers/LoginPage/LoginPage";
import PATH from "./Contstants/Path";
import { CssBaseline } from "@mui/material";
import "./styles/styles.css";
import ItemsListPage from "./Containers/ItemsListPage/ItemsListPage";
import ToDoEditor from "./Containers/ToDoEditor/ToDoEditor";
import TopBar from "./Components/TopBar/TopBar";

function App() {
  return (
    <Router>
      <CssBaseline />
      {window.localStorage.getItem("id") ? getAuthRoutes() : getPublicRoutes()}
    </Router>
  );
}

const getAuthRoutes = () => {
  return (
    <div>
      <TopBar />
      <Switch>
        <Route path={PATH.TODO_LIST} component={ItemsListPage} />
        <Route path={PATH.TODO_EDITOR} component={ToDoEditor} />
        <Route>
          <Redirect to={PATH.TODO_LIST} />
        </Route>
      </Switch>
    </div>
  );
};

const getPublicRoutes = () => {
  return (
    <Switch>
      <Route path={PATH.LOGIN} component={LoginPage} />
      <Route>
        <Redirect to={PATH.LOGIN} />
      </Route>
    </Switch>
  );
};

export default App;
