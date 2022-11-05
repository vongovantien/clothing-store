import { Button, Card, CardActionArea, CardActions, CardContent, CardMedia, Typography } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import api, { endpoint } from '../endpoints/api';

const ProductDetail = () => {
    const { id } = useParams();
    const [product, setProduct] = useState({});

    useEffect(() => {
        getProductById(id);
    }, [id])

    const getProductById = async (id) => {
        try {
            console.log(id)
            let product = await api.get(endpoint.productDetail(id))
            console.log(product.data);
            setProduct(product.data)

        } catch (error) {
            console.log(error)
        }
    }

    return (
        <div>
            <Card sx={{ maxWidth: 345 }}>
                <CardActionArea>
                    <CardMedia
                        component="img"
                        height="140"
                        image="/static/images/cards/contemplative-reptile.jpg"
                        alt="green iguana"
                    />
                    <CardContent>
                        <Typography gutterBottom variant="h5" component="div">
                            {product.name}
                        </Typography>
                        <Typography variant="body2" color="text.secondary">
                            {product.description}
                        </Typography>
                    </CardContent>
                </CardActionArea>
                <CardActions>
                    <Button size="small" color="primary">
                        Add to cart
                    </Button>
                </CardActions>
            </Card>
        </div>
    )
}

export default ProductDetail
