import AddShoppingCartIcon from '@mui/icons-material/AddShoppingCart';
import Button from '@mui/material/Button';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Typography from '@mui/material/Typography';
import { Link } from 'react-router-dom';

export default function ItemCard({ prop }) {
    return (
        <Card sx={{ maxWidth: 345 }}>
            <CardMedia
                component="img"
                alt="green iguana"
                height="140"
                image={prop.url}
            />
            <CardContent>
                <Typography gutterBottom variant="h5" component="div">
                    <Link to={`/product/${prop.id}`}>{prop.title}</Link>
                </Typography>
                <Typography variant="body2" color="text.secondary">
                    Lizards are a widespread group of squamate reptiles, with over 6,000
                    species, ranging across all continents except Antarctica
                </Typography>
            </CardContent>
            <CardActions>
                <Button size="small">Thông tin</Button>
                <Button size="small">
                    <AddShoppingCartIcon /> Thêm Vào Giỏ
                </Button>
            </CardActions>
        </Card>
    );
}
