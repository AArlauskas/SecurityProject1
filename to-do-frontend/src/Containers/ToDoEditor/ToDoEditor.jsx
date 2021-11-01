import { Button, Grid, TextField } from "@mui/material";
import { useState } from "react";
import { useHistory } from "react-router";
import { postToDoItem } from "../../Api/Api";
import PATH from "../../Contstants/Path";

export default function ToDoEditor() {
  const history = useHistory();
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const onCancel = () => history.push(PATH.TODO_LIST);
  const onSubmit = () => {
    const data = {
      title,
      description,
      isDone: false,
      userId: Number.parseInt(window.localStorage.getItem("id")),
    };
    postToDoItem(data).then((response) => {
      onCancel();
    });
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
}
