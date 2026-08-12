import { ActionsButtons } from '../../components/Buttons/ActionsButtons';

export const CategoryList = ({ category, onEdit, onDelete }) => {
    return (
        <li className="flex items-center justify-between gap-4 px-4 py-3 rounded-md bg-[#03346E] text-[#E2E2B6]">
            <div>
                <p className="font-medium">
                    <span className="font-semibold text-[#6EACDA]">Name: </span>
                    {category.name}
                </p>
                <p className="text-sm text-[#6EACDA]">
                    <span className="font-semibold text-[#6EACDA]">Description: </span>
                    {category.description}
                </p>
            </div>
            <ActionsButtons onEdit={onEdit} onDelete={onDelete} />
        </li>
    )
}