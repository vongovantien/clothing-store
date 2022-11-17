import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import { confirmable, createConfirmation } from 'react-confirm';

const ConfirmAlert = ({
    okLabel = "OK",
    cancelLabel = "Cancel",
    title = "Confirmation",
    confirmation,
    show,
    proceed,
}) => {
    return (
        <div>
            <Dialog
                open={show}
                onClose={() => proceed(false)}
                aria-labelledby="alert-dialog-title"
                aria-describedby="alert-dialog-description"
            >
                <DialogTitle id="alert-dialog-title">
                    {title}
                </DialogTitle>
                <DialogContent>
                    <DialogContentText id="alert-dialog-description">
                        {confirmation}
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={() => proceed(true)}>{okLabel}</Button>
                    <Button onClick={() => proceed(false)} autoFocus>
                        {cancelLabel}
                    </Button>
                </DialogActions>
            </Dialog>
        </div>
    );
}
export function confirm(
    confirmation,
    proceedLabel = "OK",
    cancelLabel = "cancel",
    options = {}
) {
    return createConfirmation(confirmable(ConfirmAlert))({
        confirmation,
        proceedLabel,
        cancelLabel,
        ...options
    });
}