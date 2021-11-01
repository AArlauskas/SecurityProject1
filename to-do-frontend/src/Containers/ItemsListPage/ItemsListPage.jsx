import { Grid, Button, List, Divider, Paper } from "@mui/material";
import { useEffect, useState } from "react";
import { useHistory } from "react-router";
import {
  deleteItem,
  getAdminItems,
  getUserItems,
  toggleItem,
} from "../../Api/Api";
import ToDoListItem from "../../Components/ToDoListItem/ToDoListItem";
import PATH from "../../Contstants/Path";

// const items = [
//   {
//     id: 1,
//     title: "Clean stuff",
//     description: "<script>console.log(123);</script>something nasty...",
//     isDone: false,
//   },
//   {
//     id: 2,
//     title: "Vacuum home",
//     description: "something nasty but at the same time cool...",
//     isDone: true,
//   },
//   {
//     id: 3,
//     title: "Vacuum home",
//     description:
//       "something nasty but at the same time cool...something nasty but at the same time cool...something nasty but at the same time cool...something nasty but at the same time cool...something nasty but at the same time cool...",
//     isDone: true,
//   },
// ];
export default function ItemsListPage() {
  const history = useHistory();
  const [items, setItems] = useState([]);

  useEffect(() => {
    const role = window.localStorage.getItem("role");
    if (role === "user") getUserList();
    else if (role === "admin") getAdminList();
    else {
      window.localStorage.clear();
      window.location.reload();
    }
  }, []);

  const getAdminList = () => {
    const id = window.localStorage.getItem("id");
    getAdminItems(id)
      .then((response) => {
        setItems(response.data);
      })
      .catch(() => {
        window.localStorage.clear();
        window.location.reload();
      });
  };

  const getUserList = () => {
    const id = window.localStorage.getItem("id");
    getUserItems(id)
      .then((response) => {
        setItems(response.data);
      })
      .catch(() => {
        window.localStorage.clear();
        window.location.reload();
      });
  };

  const onItemDelete = (id) => {
    deleteItem(id)
      .then(() => {
        const filteredItems = items.filter((i) => i.id !== id);
        setItems(filteredItems);
      })
      .catch(() => {
        window.localStorage.clear();
        window.location.reload();
      });
  };

  const onItemToggle = (id) => {
    toggleItem(id).then(() => {
      const tempItems = [...items];
      const item = tempItems.find((i) => i.id === id);
      item.isDone = !item.isDone;
      setItems(tempItems);
    });
  };
  return (
    <Grid container justifyContent="center" className="toDoList">
      <Grid
        container
        item
        direction="column"
        alignItems="center"
        spacing={2}
        xs={11}
        sm={9}
        md={7}
      >
        <Grid item>
          <Button
            variant="contained"
            onClick={() => history.push(PATH.TODO_EDITOR)}
          >
            Add new To Do Item
          </Button>
        </Grid>
        <Grid item>
          <Paper elevation={2}>
            <List>
              {items.map((item) => (
                <div key={item.id}>
                  <ToDoListItem
                    key={item.id}
                    id={item.id}
                    title={item.title}
                    description={item.description}
                    isDone={item.isDone}
                    onDelete={onItemDelete}
                    onToggle={onItemToggle}
                  />
                  <Divider />
                </div>
              ))}
            </List>
          </Paper>
        </Grid>
      </Grid>
    </Grid>
  );
}
