import * as React from "react";
import { Form } from "./basic/Form";
import { Field } from "./basic/Field";

export const ContactUsForm: React.SFC = () => {
    return (
        <Form
            action="http://localhost:5000/api/teams/create"
            render={() => (
                <React.Fragment>
                    <Field id="name" label="Name" />
                    <Field id="skill" label="Skill" />
                </React.Fragment>
            )}
        />
    );
};