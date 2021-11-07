import { Button, Grid, TextField } from "@mui/material";
import { useEffect, useState } from "react";
import { useHistory } from "react-router";
import { getItem, postToDoItem, updateItem } from "../../Api/Api";
import PATH from "../../Contstants/Path";
import { withRouter } from "react-router";

const ToDoEditor = (props) => {
  const history = useHistory();
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");

  useEffect(() => {
    const itemId = props.match?.params?.itemId;
    if (itemId) {
      getItem(itemId).then((response) => {
        const { data } = response;
        setTitle(data.title);
        setDescription(data.description);
      });
    }
  }, [props]);
  const onCancel = () => history.push(PATH.TODO_LIST);
  const onSubmit = () => {
    const itemId = props.match?.params?.itemId;
    const data = {
      title,
      description,
      userId: Number.parseInt(window.localStorage.getItem("id")),
    };
    if (itemId) {
      updateItem(itemId, data).then(() => {
        onCancel();
      });
    } else {
      postToDoItem(data).then((response) => {
        data.isDone = false;
        onCancel();
      });
    }
  };
  return (
    <Grid
      className="editor"
      container
      direction="column"
      alignItems="stretch"
      spacing={2}
    >
      <Grid item xs={6}>
        <TextField
          fullWidth
          label="Title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          inputProps={{
            maxLength: 50,
          }}
        />
      </Grid>
      <Grid item xs={6}>
        <TextField
          variant="outlined"
          fullWidth
          label="Description"
          multiline
          rows={3}
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          inputProps={{
            maxLength: 150,
          }}
        />
      </Grid>
      <Grid item xs={12} container justifyContent="space-around">
        <Grid item>
          <Button
            fullWidth
            variant="contained"
            color="secondary"
            onClick={onCancel}
          >
            Cancel
          </Button>
        </Grid>
        <Grid item>
          <Button
            fullWidth
            variant="contained"
            color="primary"
            onClick={onSubmit}
          >
            Confirm
          </Button>
        </Grid>
      </Grid>
    </Grid>
  );
};

export default withRouter(ToDoEditor);
