import { ActionsButtons } from '../../components/Buttons/ActionsButtons';

export const EquipmentList = ({ equipment, onEdit, onDelete }) => {
    return (
        <li className="flex items-center justify-between gap-4 px-4 py-3 rounded-md bg-[#03346E] text-[#E2E2B6]">
            <div>
                <p className="font-medium">{equipment.name}</p>
                <p className="font-medium">{equipment.brand}</p>
                <p className="font-medium">{equipment.model}</p>
                <p className="font-medium">{equipment.serialNumber}</p>
            </div>
            <ActionsButtons onEdit={onEdit} onDelete={onDelete} />
        </li>
    )
}