import {
  ListItem,
  ListItemAvatar,
  Avatar,
  ListItemText,
  ListItemSecondaryAction,
  ListItemIcon,
  IconButton,
} from "@mui/material";
import { green, pink } from "@mui/material/colors";
import InnerHTML from "dangerously-set-html-content";
import ThumbUpIcon from "@mui/icons-material/ThumbUp";
import ThumbDownIcon from "@mui/icons-material/ThumbDown";
import DeleteIcon from "@mui/icons-material/Delete";

export default function ToDoListItem({
  id,
  title,
  description,
  isDone,
  onDelete,
  onToggle,
  onClick,
}) {
  const formatedDescription = `<div style="padding: 40px">${description}</div>`;
  return (
    <ListItem button>
      <ListItemAvatar id="Avatar" onClick={() => onToggle(id)}>
        {isDone ? (
          <Avatar sx={{ bgcolor: green[500] }}>
            <ThumbUpIcon />
          </Avatar>
        ) : (
          <Avatar sx={{ bgcolor: pink[500] }}>
            <ThumbDownIcon />
          </Avatar>
        )}
      </ListItemAvatar>
      <ListItemText primary={title} onClick={() => onClick(id)} />
      <InnerHTML html={formatedDescription} onClick={() => onClick(id)} />
      <ListItemSecondaryAction>
        <ListItemIcon>
          <IconButton onClick={() => onDelete(id)}>
            <DeleteIcon />
          </IconButton>
        </ListItemIcon>
      </ListItemSecondaryAction>
    </ListItem>
  );
}
