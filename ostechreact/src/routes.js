import React from "react";
import { BrowserRouter, Routes, Route } from "react-router";
import { Category } from "./pages/Category";
import { App } from "./App";
import { Customer } from "./pages/Customer";

export default function AppRoutes() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" exact Component={App} />
                <Route path="/categories" exact Component={Category} />
                <Route path="/customers" Component={Customer} />
            </Routes>
        </BrowserRouter>
    );
}