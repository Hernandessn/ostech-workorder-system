import React from "react";
import { BrowserRouter, Routes, Route } from "react-router";
import { Category } from "./pages/Category";
import { App } from "./App";

export default function AppRoutes() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" exact Component={App} />
                <Route path="/category" exact Component={Category} />
            </Routes>
        </BrowserRouter>
    );
}